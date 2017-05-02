using System.Web.Optimization;

namespace Kapitalist.Web.Client
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*", "~/Scripts/custom/jquery.customvalidate.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-cookie").Include("~/Scripts/js-cookie/js.cookie-2.1.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.min.js", "~/Scripts/respond.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/kendo-ui").Include("~/Scripts/kendo/2016.2.504/kendo.ui.core.min.js",
                "~/Scripts/kendo/2016.2.504/cultures/kendo.culture.en.min.js",
                "~/Scripts/kendo/2016.2.504/cultures/kendo.culture.ru.min.js",
                "~/Scripts/kendo/2016.2.504/cultures/kendo.culture.uk.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/culture-helper").Include(
                      "~/Scripts/moment-with-locales.min.js",
                      "~/Scripts/moment-timezone-with-data-2010-2020.min.js",
                      "~/Scripts/custom/culture-helper.js"));

            bundles.Add(new ScriptBundle("~/bundles/home/index").Include("~/Scripts/custom/work-with-us.js"));

            bundles.Add(new ScriptBundle("~/bundles/tenders/query-blocks").Include("~/Scripts/custom/searches/tenders/tenders-query-blocks.js"));
            bundles.Add(new ScriptBundle("~/bundles/plans/query-blocks").Include("~/Scripts/custom/searches/plans/plans-query-blocks.js"));
            bundles.Add(new ScriptBundle("~/bundles/query-blocks-url-pushstate").Include("~/Scripts/custom/query-blocks-url-pushstate.js"));


            bundles.Add(new ScriptBundle("~/bundles/_layout").Include(
                "~/Scripts/custom/current-time.js",
                "~/Scripts/custom/change-culture.js"));

            bundles.Add(new ScriptBundle("~/bundles/account/registration").Include(
                "~/Scripts/custom/company-type-switcher-registration.js",
                "~/Scripts/custom/recaptcha.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile/edit-personal").Include(
                "~/Scripts/custom/company-type-switcher-edit-personal.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo-period").Include("~/Scripts/custom/kendo-period-datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo-ui-configuration").Include("~/Scripts/custom/kendo-ui-configuration.js"));

            bundles.Add(new ScriptBundle("~/bundles/jstree").Include("~/Scripts/jstree.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo-ui").Include(
                //"~/Content/kendo/2016.2.504/kendo.common.core.min.css",
                "~/Content/kendo/2016.2.504/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2016.2.504/kendo.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/jstree").Include("~/Content/jstree-themes/default/style.min.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/css/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/css/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/Site").Include("~/Content/css/Site.css"));
        }
    }
}