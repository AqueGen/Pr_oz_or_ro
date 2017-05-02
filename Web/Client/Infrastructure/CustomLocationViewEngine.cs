using System.Web.Mvc;

namespace Kapitalist.Web.Client.Infrastructure
{
    public class CustomLocationViewEngine : RazorViewEngine
    {
        public CustomLocationViewEngine()
        {
            string[] viewLocations = new string[]
            {
                "~/Views/Drafts/{1}/{0}.cshtml",
                "~/Views/Published/{1}/{0}.cshtml",
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = viewLocations;
            ViewLocationFormats = viewLocations;
            MasterLocationFormats = viewLocations;
        }

    }
}