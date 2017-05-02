using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Threading;
using System.Net;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Вимоги/Скарги на умови закупівлі
    /// </summary>
    public class TenderComplaintsService : ComplaintsService
    {
        public TenderComplaintsService(Guid tenderId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N") + "/complaints", cookieContainer)
        {
        }
    }

    /// <summary>
    /// Отримання інформації про закупівлі
    /// </summary>
    public class TendersService : DocumentsService, ITendersService
    {
        public TendersService(bool readOnly)
            : base("tenders", null, readOnly)
        {
        }

        public TendersService(CookieContainer cookieContainer = null)
            : base("tenders", cookieContainer)
        {
        }

        /// <summary>
        /// Зміна критеріїв
        /// </summary>
        /// <param name="tenderId">ідентифікатор закупівлі</param>
        /// <param name="features">нецінові критерії</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        public async Task<Tender> ChangeFeatures(Guid tenderId, Feature[] features, string acc_token)
        {
            return await PatchAsync<Tender>(new { features }, tenderId.ToString("N"), new { acc_token });
        }

        /// <summary>
        /// Cтворення початкового запису закупівлі
        /// </summary>
        /// <param name="tender">при реєстрації закупівлі у базі даних потрібно надати всі основні деталі закупівлі
        /// (окрім двійкових документів) в тілі запиту.</param>
        /// <returns>створений тендер разом с токеном власника</returns>
        public async Task<Protected<Tender>> CreateTender(Tender tender)
        {
            return await PostAsync<Protected<Tender>>(tender);
        }

        /// <summary>
        /// Видалення критеріїв
        /// </summary>
        /// <param name="tenderId">ідентифікатор закупівлі</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        public async Task<Tender> DeleteFeatures(Guid tenderId, string acc_token)
        {
            return await PatchAsync<Tender>(new { features = new Feature[0] }, tenderId.ToString("N"), new { acc_token });
        }

        /// <summary>
        /// Прочитати інформацію про окрему закупівлю
        /// </summary>
        /// <param name="id">ідентифікатор закупівлі</param>
        /// <returns>закупівлю</returns>
        public async Task<Tender> GetTender(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<Tender>(id.ToString("N"), null, cancellationToken);
        }

        /// <summary>
        /// Отримати список закупівель
        /// Повернені закупівлі просортовані за датою модифікації.
        /// Якщо запит наступної сторінки повертається без даних (наприклад, пустий масив), тоді немає сенсу викликати сторінки далі.
        /// </summary>
        /// <param name="offset">це параметр, який ви повинні додати до вихідного запиту, щоб отримати наступну сторінку.</param>
        /// <param name="limit">максимальна кількість data записів потоку даних закупівлі (розмір пакета). За промовчанням 100.</param>
        /// <returns>сторінку з масивом ідентифікаторів закупівель і часом їх останньої модифікації та посиланням на наступну сторінку</returns>
        public async Task<ModificationsPage> GetModificationsPage(DateTime? offset = null, bool reverse = false, int limit = 100, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<ModificationsPage>(new { offset, descending = reverse ? 1 : (int?)null, limit }, cancellationToken);
        }

        /// <summary>
        /// Зміна початкового запису Закупівлі
        /// </summary>
        /// <param name="id">ідентифікатор закупівлі</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        public async Task<Tender> ChangeTender(Guid id, object changes, string acc_token)
        {
            return await PatchAsync<Tender>(changes, id.ToString("N"), new { acc_token });
        }
    }
}