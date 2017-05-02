using System.Web.Mvc;
using Kapitalist.Web.Client.Controllers.Base;

namespace Kapitalist.Web.Client.Controllers
{
    public class HomeController : BaseController
    {

        public PartialViewResult GetTemplate(string template)
        {
            return PartialView($"Searches/QueryComponents/{template}Partial");
        }

        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}