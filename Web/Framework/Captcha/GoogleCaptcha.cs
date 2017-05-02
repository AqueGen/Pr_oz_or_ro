using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json;
using Kapitalist.Web.Resources;

namespace Kapitalist.Web.Framework.Captcha
{
    public class GoogleCaptcha
    {
        private const string PrivateKey = "6LdoxSATAAAAAN2vXYjYuQn1ObhiP9LVIG2vIZ-T";
        private const string GoogleVerifyUrl = "https://www.google.com/recaptcha/api/siteverify";
        public const string PublicKey = "6LdoxSATAAAAAHNTiUgxRXiKXpDZ17H6t_HkT3Xt";
        public const string GoogleRequestKey = "g-recaptcha-response";

        private GoogleReCaptchaJson _captcha { get; set; }

        public string ErrorMessage { get; set; }

        public bool Success { get; set; }

        public GoogleCaptcha(string recaptchaResponse)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] response = webClient.UploadValues(GoogleVerifyUrl, new NameValueCollection()
                {
                    { "secret", PrivateKey },
                    { "response", recaptchaResponse },
                    { "remoteip", "" }
                });

                var result = System.Text.Encoding.UTF8.GetString(response);

                _captcha = JsonConvert.DeserializeObject<GoogleReCaptchaJson>(result);



                ErrorMessage = GetErrorMessage(_captcha);
                Success = _captcha.Success || !_captcha.Success && string.IsNullOrWhiteSpace(ErrorMessage);
            }
        }


        private string GetErrorMessage(GoogleReCaptchaJson captcha)
        {

            if (captcha.ErrorCodes == null || captcha.ErrorCodes.Count <= 0)
                return string.Empty;

            var error = captcha.ErrorCodes[0].ToLower();
            switch (error)
            {
                case ("missing-input-secret"):
                    return GlobalRes.missing_input_secret;
                case ("invalid-input-secret"):
                    return GlobalRes.invalid_input_secret;
                case ("missing-input-response"):
                    return GlobalRes.missing_input_response;
                case ("invalid-input-response"):
                    return GlobalRes.invalid_input_response;
                default:
                    return GlobalRes.error_occured;
            }
        }

        private class GoogleReCaptchaJson
        {
            [JsonProperty("success")]
            public bool Success { get; set; }
            [JsonProperty("challenge_ts")]
            public string ChallengeTs { get; set; }
            [JsonProperty("hostname")]
            public string Hostname { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}