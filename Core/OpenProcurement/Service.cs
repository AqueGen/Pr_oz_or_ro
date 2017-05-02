using Kapitalist.Core.OpenProcurement.Converters;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Абстрактний базовий клас для всіх сервісів OpenProcurement
    /// </summary>
    public abstract class Service : IDisposable
    {
        private readonly CookieContainer _cookieContainer;
        private const string APIKey = "Prozorro:Key";
        private const string APIMode = "Prozorro:Mode";
        protected Modes Mode { get; }
        protected readonly HttpClient _client;

        /// <summary>
        /// Дані синхронізуються на кластері з декількох серверів.
        /// Для узгодження між окремими запитами до ЦБД важливо, щоб клієнт працював завжди з одним сервером.
        /// Тому обов’язково використовувати реп’яшок (сookie) при подачі POST/PUT/PATCH/DELETE запитів.
        /// http://api-docs.openprocurement.org/uk_UA/latest/cluster.html#cluster
        /// </summary>
        /// <param name="path">відносний шлях сервісу</param>
        /// <param name="cookieContainer">контейнер репяшків для безпечних транзакцій в кластері</param>
        /// <param name="readOnly">true - якщо тільки для синхронізації локальної БД</param>
        protected Service(string path, CookieContainer cookieContainer, bool readOnly = false)
        {
            _cookieContainer = cookieContainer ?? new CookieContainer();
            Modes mode;
            if (Enum.TryParse(ConfigurationManager.AppSettings[APIMode], true, out mode))
                Mode = mode;
            if (readOnly)
            {
                _client = new HttpClient
                {
                    BaseAddress = new Uri((Mode == Modes.Live
                        ? "https://public.api.openprocurement.org/api/2.3/"
                        : "https://api-sandbox.openprocurement.org/api/2.3/")
                                          + path)
                };
            }
            else
            {
                _client =
                    new HttpClient(new HttpClientHandler { CookieContainer = _cookieContainer })
                    {
                        BaseAddress = new Uri((Mode == Modes.Live
                            ? "https://lb.api.openprocurement.org/api/2.3/"
                            : "https://lb.api-sandbox.openprocurement.org/api/2.3/")
                                              + path)
                    };
            }
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string key = ConfigurationManager.AppSettings[APIKey];
            if (key != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes(key + ':')));
        }

        protected string GetRequestUri(string relativeUri, object query)
        {
            string uri = null;
            if (relativeUri != null)
            {
                uri = _client.BaseAddress.Segments.Last();
                if (uri.EndsWith("/"))
                    uri = relativeUri;
                else
                    uri += "/" + relativeUri;
            }
            if (query != null)
            {
                var queryString = string.Join("&", new RouteValueDictionary(query)
                    .Where(x => x.Value != null)
                    .Select(x => x.Key + '=' +
                    HttpUtility.UrlEncode(x.Value is DateTime
                        ? DateTimeConverter.ToLfeString((DateTime)x.Value)
                        : x.Value.ToString())).ToArray());
                if (!string.IsNullOrEmpty(queryString))
                    uri += "?" + queryString;
            }
            return uri;
        }

        protected async Task<T> GetAsync<T>(object query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<T>(null, query, cancellationToken);
        }

        protected async Task<T> GetAsync<T>(string relativeUri, object query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new HttpRequestMessage(HttpMethod.Get, GetRequestUri(relativeUri, query));
            return await SendAsync<T>(message, cancellationToken);
        }

        protected async Task<T> PostAsync<T>(object data, string relativeUri = null, object query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new HttpRequestMessage(HttpMethod.Post, GetRequestUri(relativeUri, query))
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { data },
                    Formatting.None,
                    Settings.SerializerSettings),
                    Encoding.UTF8, "application/json")
            };

            return await SendAsync<T>(message, cancellationToken, true);
        }

        protected async Task<T> PatchAsync<T>(object data, string relativeUri = null, object query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            string s = JsonConvert.SerializeObject(new { data },
                    Formatting.None,
                    Settings.SerializerSettings);

            var message = new HttpRequestMessage(new HttpMethod("PATCH"), GetRequestUri(relativeUri, query))
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { data },
                    Formatting.None,
                    Settings.SerializerSettings),
                    Encoding.UTF8, "application/json")
            };
            return await SendAsync<T>(message, cancellationToken, true);
        }

        protected async Task<T> DeleteAsync<T>(string relativeUri, object query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, GetRequestUri(relativeUri, query));
            return await SendAsync<T>(message, cancellationToken, true);
        }

        private async Task<T> SendAsync<T>(HttpRequestMessage message, CancellationToken cancellationToken = default(CancellationToken), bool clasterSafe = false)
        {
            // Реп’яшки (сookies) забезпечують прив’язку до сервера. Такий реп’яшок можна отримати через GET запит,
            // а тоді використовувати його в POST/PUT/PATCH/DELETE.
            if (clasterSafe && _cookieContainer.Count < 2)
                await _client.GetAsync(string.Empty, cancellationToken);

            var result = await _client.SendAsync(message, cancellationToken);
            return await result.Content.ReadAsStringAsync().ContinueWith(x =>
            {
                if (result.IsSuccessStatusCode)
                {
                    try
                    {
                        if (typeof(T) == typeof(bool))
                            return (T)(object)true;
                        else if (typeof(T).GetInterfaces().Any(i => i == typeof(IRootModel)))
                            return JsonConvert.DeserializeObject<T>(x.Result, Settings.SerializerSettings);
                        else
                            return JsonConvert.DeserializeObject<Container<T>>(x.Result, Settings.SerializerSettings).Data;
                    }
                    catch (Exception ex)
                    {
                        throw new APIJsonException(typeof(T), ex, x.Result);
                    }
                }
                else
                {
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.GatewayTimeout:
                            throw new APIGatewayTimeoutException();
                        case HttpStatusCode.PreconditionFailed:
                            throw new APIServerChangedException();
                        case (HttpStatusCode)429:
                            throw new APITooManyRequestsException();
                        default:
                            ErrorsResponce errorsResponce = null;
                            try
                            {
                                errorsResponce = JsonConvert.DeserializeObject<ErrorsResponce>(x.Result);
                            }
                            catch
                            {
                                throw new APIStatusCodeException(result.StatusCode, result.ReasonPhrase);
                            }
                            throw new APIErrorsException(result.StatusCode, result.ReasonPhrase, errorsResponce);
                    }
                }
            }, cancellationToken);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        protected enum Modes
        {
            SandBox = 0,
            Live = 1
        }
    }
}