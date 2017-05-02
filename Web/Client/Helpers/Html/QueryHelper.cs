using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Web.Resources;

namespace Kapitalist.Web.Client.Helpers.Html
{
    public static class QueryHelper
    {
        public static IHtmlString GenerateQueryComponentString(this HtmlHelper helper, string name, string value)
        {
            GlobalRes.ResourceManager.IgnoreCase = true;
            var localizedName = GlobalRes.ResourceManager.GetString(name);

            var divBuilder = new TagBuilder("div");
            divBuilder.AddCssClass("block input-group");

            var labelBuilder = new TagBuilder("label");
            labelBuilder.AddCssClass("input-group-addon control-label");
            labelBuilder.SetInnerText(localizedName);

            var inputBuilder = new TagBuilder("input");
            inputBuilder.AddCssClass("form-control query");
            inputBuilder.MergeAttribute("type", "text");
            inputBuilder.MergeAttribute("name", name);
            inputBuilder.MergeAttribute("value", value);

            var deleteBuilder = new TagBuilder("a");
            deleteBuilder.AddCssClass("delete input-group-addon col-xd-1");

            var plusBuilder = new TagBuilder("i");
            plusBuilder.AddCssClass("fa fa-times");

            divBuilder.AddInnerTagBuilder(labelBuilder);
            divBuilder.AddInnerTagBuilder(inputBuilder);

            deleteBuilder.AddInnerTagBuilder(plusBuilder);
            divBuilder.AddInnerTagBuilder(deleteBuilder);

            return new MvcHtmlString(divBuilder.ToString());
        }

        public static void AddInnerTagBuilder(this TagBuilder parenTagBuilder, TagBuilder childTagBuilder)
        {
            parenTagBuilder.InnerHtml += Environment.NewLine;
            parenTagBuilder.InnerHtml += childTagBuilder.ToString();
            parenTagBuilder.InnerHtml += Environment.NewLine;
        }

        public static IEnumerable<Query> ToQueryList(string query)
        {
            query = HttpUtility.UrlDecode(query);
            var newList = new List<Query>();
            if (string.IsNullOrWhiteSpace(query))
                return newList;


            query = query.Remove(0, 1);
            var queryes = query.Split('&');

            foreach (var item in queryes)
            {
                var itemQuery = item.Split('=');
                if (!item.Contains("StartDate") && !item.Contains("EndDate"))
                {
                    newList.Add(new Query(itemQuery[0], itemQuery[1]));
                }
            }
            return newList;
        }

        public class Query
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public Query(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public Query()
            {
            }
        }
    }
}