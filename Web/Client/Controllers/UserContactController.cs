using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.Controllers.Drafts;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("userContact")]
    public class UserContactController : BaseDraftController
    {
        private IProfileProvider ProfileProvider { get; }

        public UserContactController(IProfileProvider profileProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            ProfileProvider = profileProvider;
        }


        [Route]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            var contacts = await ProfileProvider.GetContacts(userOrganizationId);
            IEnumerable<ContactViewModel> contactViewModels = contacts.Select(m => new ContactViewModel(m));
            return View(contactViewModels);
        }

        [Route("addContact")]
        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new ContactViewModel();
            return View(viewModel);
        }

        [Route("addContact")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(ContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var contactDTO = viewModel.ToDTO();
            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            await ProfileProvider.AddContact(userOrganizationId, contactDTO);

            return RedirectToAction("Index");
        }


        [Route("editContact/{contactId}")]
        [HttpGet]
        public async Task<ActionResult> Edit(int contactId)
        {
            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            var contact = await ProfileProvider.GetContact(userOrganizationId, contactId);

            var viewModel = new ContactViewModel(contact);
            return View(viewModel);
        }


        [Route("editContact/{contactId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ContactViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            var contactDTO = viewModel.ToDTO();
            await ProfileProvider.EditContact(userOrganizationId, contactDTO);

            return RedirectToAction("Index");
        }

        [Route("deleteContact/{contactId}")]
        [HttpPost]
        public async Task<ActionResult> Delete(int contactId)
        {
            int userOrganizationId = AccessManager.Value.UserOrganizationId;
            await ProfileProvider.DeleteContact(userOrganizationId, contactId);
            return RedirectToAction("Index");
        }
    }
}
