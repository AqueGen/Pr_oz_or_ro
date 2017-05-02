using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.ViewModels.Profile;
using Kapitalist.Web.Security.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Web.Client.ViewModels.Account;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("profile")]
    [Authorize]
    public class ProfileController : BaseController
    {
        private ApplicationSignInManager _signInManager;

        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        public Lazy<IProfileProvider> ProfileProvider { get; }

        public Lazy<IDraftProvider> DraftProvider { get; }
        public Lazy<ITenderProvider> TenderProvider { get; set; }

        public ProfileController(Lazy<IProfileProvider> profileProvider, Lazy<IDraftProvider> draftProvider, Lazy<ITenderProvider> tenderProvider)
        {
            ProfileProvider = profileProvider;
            DraftProvider = draftProvider;
            TenderProvider = tenderProvider;
        }

        [Route("changePassword")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Route("changePassword")]
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var result =
                await
                    UserManager.ChangePasswordAsync(User.Identity.GetUserId(), viewModel.CurrentPassword,
                        viewModel.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Profile");
            }
            AddErrors(result);
            return View();
        }

        [Route("edit")]
        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var userId = User.GetUserOrganizationId();
            var userOrganizationDTO = await ProfileProvider.Value.GetUserOrganization(userId);
            var viewModel = new PersonalViewModel(userOrganizationDTO);

            var schemesProvider = DependencyResolver.Current.GetService<ISchemesProvider>();
            var schemeList = (await schemesProvider.GetIdentifierSchemes())
                    .Select(m => new SelectListItem { Value = m, Text = m, Selected = m == viewModel.Company.Scheme});
            var schemes = new List<SelectListItem>(schemeList);
            ViewData["Schemes"] = schemes;

            return View(viewModel);
        }

        [Route("edit")]
        [HttpPost]
        public ActionResult Edit(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) // Will have a Model Error "ReCaptcha" if the user input is incorrect
                return View(viewModel);

            return RedirectToAction("Personal");
        }

        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [Route("memberTenders")]
        [HttpGet]
        public ActionResult MemberTenders()
        {
            TendersViewModel tendersViewModel = new TendersViewModel();

            return View("Tenders", tendersViewModel);
        }

        [Route("messages")]
        [HttpGet]
        public ActionResult Messages()
        {
            MessageViewModel messageViewModel = new MessageViewModel();

            return View("Messages", messageViewModel);
        }

        [Route("myPlanDrafts")]
        [HttpGet]
        public async Task<ActionResult> MyPlanDrafts()
        {
            int userOrganizationId = User.GetUserOrganizationId();

            var draftList = await DraftProvider.Value.GetDraftPlans(userOrganizationId);
            DraftsViewModel viewModel = new DraftsViewModel();
            List<Guid> guids = draftList.Select(m => m.Guid).ToList();
            viewModel.Drafts = guids;
            return View("PlanDrafts", viewModel);
        }

        [Route("myTenderDrafts")]
        [HttpGet]
        public async Task<ActionResult> MyTenderDrafts()
        {
            int userOrganizationId = User.GetUserOrganizationId();

            var draftList = await DraftProvider.Value.GetDraftTenders(userOrganizationId);
            DraftsViewModel viewModel = new DraftsViewModel();
            List<Guid> guids = draftList.Select(m => m.Guid).ToList();
            viewModel.Drafts = guids;
            return View("TenderDrafts", viewModel);
        }

        [Route("myTenders")]
        [HttpGet]
        public async Task<ActionResult> MyTenders()
        {
            int userOrganizationId = User.GetUserOrganizationId();

            var tenders = await TenderProvider.Value.GetTenders(userOrganizationId);
            var viewModel = new TendersViewModel();
            List<Guid> guids = tenders.Select(m => m.Guid).ToList();
            viewModel.Tenders = guids;

            return View("Tenders", viewModel);
        }

        [Route("personal")]
        [HttpGet]
        public async Task<ActionResult> Personal()
        {
            //var userId = User.GetUserOrganizationId();
            //var userOrganizationDTO = await ProfileProvider.Value.GetUserOrganization(userId);
            //var viewModel = new PersonalViewModel(userOrganizationDTO);
            //return View(viewModel);
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}