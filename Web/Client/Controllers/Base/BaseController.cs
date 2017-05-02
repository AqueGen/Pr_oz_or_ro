using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Exceptions;
using Kapitalist.Web.Framework.Helpers;
using Nito.AspNetBackgroundTasks;

namespace Kapitalist.Web.Client.Controllers.Base
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            if (exception is ForbiddenException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = ForbiddenError();
            }
            else if (exception is BadRequestException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = ErrorBadRequest();
            }
            else if (exception is InternalServerErrorException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = InternalServerError();
            }
            else if (exception is MethodNotAllowedException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = MethodNotAllowedError();
            }
            else if (exception is NotFoundException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = NotFoundError();
            }
            else if (exception is UnauthorizedException)
            {
                filterContext.ExceptionHandled = true;
                filterContext.Result = UnauthorizedError();
            }
        }

        public ActionResult DisplayConfirmation(string title, string message, string description = null)
        {
            TempData["Title"] = title;
            TempData["Message"] = new MvcHtmlString(message);
            if (description != null)
                TempData["Description"] = new MvcHtmlString(description);
            return RedirectToAction("Confirmation", "Messages");
        }

        public ActionResult DisplayError(string message)
        {
            return DisplayError(null, message);
        }

        public ActionResult DisplayError(string title, string message, string description = null)
        {
            if (title != null)
                TempData["Title"] = new MvcHtmlString(title);
            TempData["Message"] = new MvcHtmlString(message);
            if (description != null)
                TempData["Description"] = new MvcHtmlString(description);
            return RedirectToAction("Error", "Messages");
        }





        protected void AddError(string key, Exception exception)
        {
            ModelState.AddModelError(key, exception);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            SetCurrentCulture();
            return base.BeginExecuteCore(callback, state);
        }

        /// <summary>
        /// Відправити асинхронно електронний лист
        /// </summary>
        /// <param name="to">адресат</param>
        /// <param name="viewPath">вигляд/шаблон, по якому потрібно згенерувати лист</param>
        /// <param name="model">модель даних, які потрібно відобразити в листі</param>
        /// <returns>true - якщо лист був успішно відправлений, інакше - false</returns>
        protected async Task<bool> SendEmailAsync(string to, string viewPath, object model = null)
        {
            SetCurrentCulture();
            using (var mail = this.RenderMail(viewPath, model))
            {
                return await mail.SendAsync(to);
            }
        }

        protected string GetCurrentCulture()
        {
            string currentCulture;

            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                currentCulture = cultureCookie.Value;
            else
                currentCulture = Request.UserLanguages != null && Request.UserLanguages.Length > 0
                    ? Request.UserLanguages[0]
                    : // obtain it from HTTP header AcceptLanguages
                    null;
            return currentCulture;
        }


        protected void SetCurrentCulture()
        {
            string currentCulture = GetCurrentCulture();
            // Validate culture name
            currentCulture = CultureHelper.GetImplementedCulture(currentCulture); // This is safe

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentCulture);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        /// <summary>
        /// Відправити асинхронно електронний лист, не чикаючи результату відправки.
        /// </summary>
        /// <param name="to">адресат</param>
        /// <param name="viewPath">вигляд/шаблон, по якому потрібно згенерувати лист</param>
        /// <param name="model">модель даних, які потрібно відобразити в листі</param>
        protected void StartEmailSending(string to, string viewPath, object model = null)
        {
            SetCurrentCulture();
            var mail = this.RenderMail(viewPath, model);
            // http://blog.stephencleary.com/2012/12/returning-early-from-aspnet-requests.html
            BackgroundTaskManager.Run(() =>
            {
                try
                {
                    mail.Send(to);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Email sending to {0} error! {1}", to, ex);
                }
                finally
                {
                    try
                    {
                        mail.Dispose();
                    }
                    catch
                    {
                    }
                }
            });
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            SetCurrentCulture();
            return base.View(viewName, masterName, model);
        }

        protected override ViewResult View(IView view, object model)
        {
            SetCurrentCulture();
            return base.View(view, model);
        }

        public ActionResult ForbiddenError()
        {
            return View("Errors/Forbidden");
        }

        public ActionResult ErrorBadRequest()
        {
            return View("Errors/BadRequest");
        }

        public ActionResult InternalServerError()
        {
            return View("Errors/InternalServerError");
        }
        public ActionResult MethodNotAllowedError()
        {
            return View("Errors/MethodNotAllowed");
        }
        public ActionResult NotFoundError()
        {
            return View("Errors/NotFound");
        }
        public ActionResult UnauthorizedError()
        {
            return View("Errors/Unauthorized");
        }
    }
}