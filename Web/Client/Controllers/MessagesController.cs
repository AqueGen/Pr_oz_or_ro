using System.Web.Mvc;
using Kapitalist.Web.Client.Controllers.Base;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("message")]
    public class MessagesController : BaseController
	{
        [Route("confirmation")]
        public ActionResult Confirmation()
        {
			if (TempData["Title"] == null && TempData["Message"] == null)
			{
				return Redirect("~/");
			}
			ViewBag.NoReturns = true;
			ViewBag.Title = TempData["Title"];
			ViewBag.Message = TempData["Message"];
			ViewBag.Description = TempData["Description"];
			return View();
		}

        [Route("error")]
        public ActionResult Error()
		{
			if (TempData["Title"] == null && TempData["Message"] == null)
			{
				return Redirect("~/");
			}
			ViewBag.NoReturns = true;
			ViewBag.Title = TempData["Title"];
			ViewBag.Message = TempData["Message"];
			ViewBag.Description = TempData["Description"];
			return View();
		}
	}
}