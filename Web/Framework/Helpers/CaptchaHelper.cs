using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;
using Recaptcha;
using Kapitalist.Web.Framework.Captcha;

namespace Kapitalist.Web.Framework.Helpers
{
    public static class CaptchaHelper
    {
        /// <summary>
        /// Html Helper to build and render the Captcha control
        /// </summary>
        /// <param name="helper">HtmlHelper class provides a set of helper methods whose purpose is to help you create HTML controls programmatically</param>
        /// <returns></returns>
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper helper)
        {
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", "g-recaptcha");
            div.MergeAttribute("class", "g-recaptcha");
            div.MergeAttribute("data-sitekey", GoogleCaptcha.PublicKey);
            div.MergeAttribute("data-callback", "correctCaptcha");
            return new MvcHtmlString(div.ToString());
        }
    }
}
