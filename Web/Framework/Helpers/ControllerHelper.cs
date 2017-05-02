using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;

namespace Kapitalist.Web.Framework.Helpers
{
	public static class ControllerHelper
	{
		private static void Render(this Controller controller, TextWriter textWriter, string viewPath, object model = null, bool partial = false)
		{

			// first find the ViewEngine for this view
			ViewEngineResult viewEngineResult = null;
			if (partial)
				viewEngineResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewPath);
			else
				viewEngineResult = ViewEngines.Engines.FindView(controller.ControllerContext, viewPath, null);
			if (viewEngineResult == null)
				throw new FileNotFoundException("View cannot be found.");

			// get the view and attach the model to view data
			var view = viewEngineResult.View;

			controller.ViewData.Model = model;
			var ctx = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, textWriter);
			view.Render(ctx, textWriter);
		}

		private static string RenderToString(this Controller controller, string viewPath, object model = null, bool partial = false)
		{
			using (StringWriter sw = new StringWriter())
			{
				Render(controller, sw, viewPath, model, partial);
				return sw.ToString();
			}
		}

		public static MailMessage RenderMail(this Controller controller, string viewPath, object model = null, bool partial = false)
		{
			string html = controller.RenderToString("Mails\\" + viewPath, model, partial);
			Uri baseUrl = new Uri(controller.Request.Url.GetLeftPart(UriPartial.Authority));
			html = PreMailer.Net.PreMailer.MoveCssInline(baseUrl, html).Html;
			return MailHelper.CreateMailMessage(controller.ViewBag.Title, html, controller.ViewBag.PlainText);
		}

		public static HelperResult RenderPlainText(this WebViewPage page, MvcHtmlString sign = null)
		{
			const string plainText = "PlainText";
			page.ViewContext.ViewBag.Title = page.ViewBag.Title;
			if (page.IsSectionDefined(plainText))
			{
				page.ViewContext.ViewBag.PlainText = WebUtility.HtmlDecode(
					page.RenderSection(plainText).ToString() + sign).Replace("    ", "");
				return null;
			}
			else
			{
				return null;
			}
		}

	}
}
