using System;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class ContactController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public ContactController(ITenderProvider tenderProvider, Lazy<IAccessManager> accessManager)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/addContact")]
        [HttpGet]
        public ActionResult AddContact(Guid tenderGuid)
        {
            var viewModel = new ContactViewModel();
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addContact")]
        [HttpPost]
        public ActionResult AddContact(ContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contactDTO = viewModel.ToDTO();
            return RedirectToAction("Info", "TenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/deleteContact/{contactId}")]
        [HttpPost]
        public ActionResult DeleteContact(Guid tenderGuid, string contactId)
        {
            return RedirectToAction("Info", "TenderInfo", new {tenderGuid = tenderGuid});
        }
    }
}