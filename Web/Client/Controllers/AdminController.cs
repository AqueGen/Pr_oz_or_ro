using System.Web.Mvc;
using Kapitalist.Web.Client.Controllers.Base;

namespace Kapitalist.Web.Client.Controllers
{
    [Authorize(Roles = "SeniorAdministrator, Administrator")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}
    
