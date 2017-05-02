using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Security;
using Kapitalist.Web.Client.ViewModels.Published;


namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class CancelTenderController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }
        private IAccessManager AccessManager { get; set; }

        public CancelTenderController(ITenderProvider tenderProvider, IAccessManager accessManager)
        {
            TenderProvider = tenderProvider;
            AccessManager = accessManager;
        }

        [Route("tender/{tenderGuid:guid}/cancel")]
        [HttpGet]
        public ActionResult Cancel(Guid tenderGuid)
        {
            CancelTenderViewModel viewModel = new CancelTenderViewModel(tenderGuid);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/cancel")]
        [HttpPost]
        public ActionResult Cancel(CancelTenderViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var fileName = Path.GetFileName(viewModel.DescriptionUploadFile.FileName);
            var path = Path.Combine(Server.MapPath("~/Documents/Files"), fileName);

            if (!Directory.Exists(Server.MapPath("~/Documents/Files")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Documents/Files"));
            }
            viewModel.DescriptionUploadFile.SaveAs(path);

            return RedirectToAction("Index", "Tenders");
        }
    }
}