using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftContactController : BaseDraftController
    {
        private IDraftProvider DraftProvider { get; }
        public Lazy<IProfileProvider> ProfileProvider { get; set; }

        public DraftContactController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager, Lazy<IProfileProvider> profileProvider)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
            ProfileProvider = profileProvider;
        }

        [Route("tender/{tenderGuid:guid}/addContact")]
        [HttpGet]
        public async Task<ActionResult> AddContact(Guid tenderGuid)
        {
            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            var contacts = await ProfileProvider.Value.GetContacts(userOrganizationId, tenderGuid);
            var viewModel = new AddContactViewModel(tenderGuid, contacts.Select(m => new ContactViewModel(tenderGuid, m)));
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addContact")]
        [HttpPost]
        public async Task<ActionResult> AddContact(AddContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await DraftProvider.AddContact(viewModel.TenderGuid, viewModel.ContactId);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/deleteContact/{contactId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteContact(Guid tenderGuid, int contactId)
        {
            await DraftProvider.DeleteContact(tenderGuid, contactId);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = tenderGuid});
        }
    }
}