using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Kapitalist.Web.Client.Helpers.Html
{
    public static class LinkHelper
    {
        public static IHtmlString AddActionLink(this AjaxHelper helper, string value, string actionName,
            object routeValue, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("i");
            builder.AddCssClass("fa fa-plus-circle");

            var link =
                helper.ActionLink("[replaceme]", actionName, routeValue, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder + value));
        }

        public static IHtmlString RemoveActionLink(this AjaxHelper helper, string value, string actionName,
            object routeValue, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("i");
            builder.AddCssClass("fa fa-minus-circle");

            var link =
                helper.ActionLink("[replaceme]", actionName, routeValue, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder + value));
        }

        public static IHtmlString DeleteActionLink(this AjaxHelper helper, string value, string actionName,
            object routeValue, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var builder = new TagBuilder("i");
            builder.AddCssClass("fa fa-trash");

            var link =
                helper.ActionLink("[replaceme]", actionName, routeValue, ajaxOptions, htmlAttributes).ToHtmlString();
            return new MvcHtmlString(link.Replace("[replaceme]", builder + value));
        }
    }
}