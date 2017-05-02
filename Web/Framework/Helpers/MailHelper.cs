using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Web.Framework.Helpers
{
	public static class MailHelper
	{
		public static readonly MailAddress Sender = ConfigurationManager.AppSettings["WEBSITE_SLOT_NAME"] == "Production"
				? new MailAddress("test.capitalist@gmail.com", "E-Capital")
				: new MailAddress("test.capitalist@gmail.com", "Test E-Capital");

		public static async Task<bool> SendAsync(this MailMessage mail, string to = null)
		{
			Task<bool> f = new Task<bool>(() => {
				return Send(mail, to);
			});
			f.Start();
			return await f;
		}

		public static void StartSendEmail(this MailMessage mail, string to = null)
		{
			Action action = () => {
				Send(mail, to);
			};
			action.BeginInvoke(null, null);
		}

		public static bool Send(this MailMessage mail, string to = null)
		{
			try
			{
				if (to != null)
				{
					mail.To.Clear();
					mail.To.Add(to.ToLower());
				}
				SmtpClient smpt = new SmtpClient {
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					Credentials = new NetworkCredential("test.capitalist@gmail.com", "GhjpjhJ!")
				};
//#if !DEBUG
				try {
					smpt.Send(mail);
				}
				catch (Exception ex) {
					Trace.TraceWarning("Cannot send email. Trying to send it again without attachs. " + ex.ToString());
					mail.AlternateViews.Clear();
					mail.Attachments.Clear();
					smpt.Send(mail);
				}
//#endif
				return true;
			}
			catch (Exception ex)
			{
				// note tracing error in this case can couse dead loop then fails to send error mail
				// so we will send this error with other warnings at once later
				Trace.TraceWarning("Error! Cannot send email. " + ex.ToString());
				return false;
			}
		}

		public static MailMessage CreateMailMessage(string subject, string html, string text = null)
		{
			MailMessage mail = new MailMessage();
			mail.From = Sender;
			mail.Subject = subject;
			if (string.IsNullOrWhiteSpace(text))
			{
				mail.IsBodyHtml = true;
				mail.Body = html;
			}
			else
			{
				mail.IsBodyHtml = false;
				mail.Body = text;
				mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
			}
			return mail;
		}
	}
}

