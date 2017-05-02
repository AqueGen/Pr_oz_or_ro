using Kapitalist.Data.Models.DTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.Models.Identity;
using Kapitalist.Web.Client.ViewModels.Account;
using Kapitalist.Web.Framework.Captcha;
using Kapitalist.Web.Framework.Enums;
using Kapitalist.Web.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Web.Client.Mappers;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : BaseController
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

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [Route("confirmEmail")]
        [HttpGet]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(userId);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return DisplayConfirmation(GlobalRes.Congratulation,
                        GlobalRes.RegistrationCompleted,
                        string.Format(GlobalRes.YouCanChangeDataIn, GlobalRes.PersonalCabinet));
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("ConfirmEmail error. " + ex);
            }
            return DisplayError(null, GlobalRes.RegistrationError, GlobalRes.TrayAgainLater);
        }

        [Route("forgotPassword")]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("forgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassportViewModel viewModel)
        {
            return View(viewModel);
        }

        [Route("login")]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SignInStatus result;
            ApplicationUser user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
                result = SignInStatus.Failure;
            else
            {
                if (await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    result =
                        await
                            SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                                shouldLockout: false);
                }
                else
                    result = SignInStatus.RequiresVerification;
            }

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    if (await SendConfirmationEmail(user))
                    {
                        return DisplayConfirmation(GlobalRes.Confirmation,
                            string.Format(GlobalRes.ConfirmationEmailSent, user.Email),
                            GlobalRes.ConfirmationEmailSentDescr);
                    }
                    else
                    {
                        return DisplayError(null,
                            GlobalRes.ConfirmationEmailSendError,
                            GlobalRes.TrayAgainLater);
                    }
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", GlobalRes.InvalidloginAttempt);
                    return View(model);
            }
        }

        [Route("logoff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        [Route("register")]
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var provider = DependencyResolver.Current.GetService<ISchemesProvider>();
            var viewModel = new RegisterViewModel
            {
                CompanyType = CompanyType.Corporation
            };

            var schemeList = (await provider.GetIdentifierSchemes()).Select(m => new SelectListItem { Value = m, Text = m });
            var schemes = new List<SelectListItem>(schemeList);
            var uaEdr = schemes.FirstOrDefault(m => m.Value == "UA-EDR");
            uaEdr.Selected = true;

            ViewData["Schemes"] = schemes;

            return View(viewModel);
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            string recaptchaMessage = Request.Form[GoogleCaptcha.GoogleRequestKey];
            var recaptcha = new GoogleCaptcha(recaptchaMessage);

            if (recaptcha.Success)
            {
                if (ModelState.IsValid)
                {
                    var userOrganization = new OrganizationDTO
                    {
                        Address = viewModel.Address.ToDTO(),
                        ContactPoint = viewModel.ContactPoint.ToDTO(),
                        Name = viewModel.Company.LegalName,
                        Kind = viewModel.Kind,
                        NameEn = viewModel.Company.LegalNameEn,
                        Identifier = new IdentifierDTO
                        {
                            Id = viewModel.Company.Id,
                            Scheme = viewModel.Company.Scheme,
                            Uri = viewModel.Company.Uri,
                            LegalName = viewModel.Company.LegalName,
                            LegalNameEn = viewModel.Company.LegalNameEn
                        }
                    };

                    var provider = DependencyResolver.Current.GetService<ITenderProvider>();
                    var userOrganizationId = await provider.AddUserOrganization(userOrganization);
                    var user = new ApplicationUser
                    {
                        UserName = viewModel.Email,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.Phone,
                        UserOrganizationId = userOrganizationId
                    };
                    var result = await UserManager.CreateAsync(user, viewModel.Password);
                    if (result.Succeeded)
                    {
                        if (await SendConfirmationEmail(user))
                        {
                            return DisplayConfirmation(GlobalRes.Confirmation,
                                string.Format(GlobalRes.ConfirmationEmailSent, user.Email),
                                GlobalRes.ConfirmationEmailSentDescr);
                        }
                        else
                        {
                            return DisplayError(null,
                                GlobalRes.ConfirmationEmailSendError,
                                GlobalRes.TrayAgainLater);
                        }
                    }
                    AddErrors(result);
                    await provider.DeleteUserOrganization(userOrganizationId);
                }
            }
            else
            {
                ModelState.AddModelError(nameof(viewModel.CaptchaSuccess), recaptcha.ErrorMessage);
            }

            var schemesProvider = DependencyResolver.Current.GetService<ISchemesProvider>();
            var schemeList = (await schemesProvider.GetIdentifierSchemes())
                    .Select(m => new SelectListItem { Value = m, Text = m });
            var schemes = new List<SelectListItem>(schemeList);
            var uaEdr = schemes.FirstOrDefault(m => m.Value == "UA-EDR");
            uaEdr.Selected = true;
            ViewData["Schemes"] = schemes;
            return View(viewModel);
        }

        [Route("ConfirmEmail")]
        public async Task<bool> SendConfirmationEmail(ApplicationUser user)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code },
                protocol: Request.Url.Scheme);
            return await SendEmailAsync(user.Email, "ConfirmEmail", callbackUrl);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public string LoginProvider { get; set; }

            public string RedirectUri { get; set; }

            public string UserId { get; set; }

            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion Helpers
    }
}