using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	public class APIErrorsException : APIStatusCodeException
	{
		public Error[] Errors { get; }

		internal APIErrorsException(HttpStatusCode statusCode, string reasonPhrase, ErrorsResponce responce)
			: base(statusCode, reasonPhrase)
		{
			if (responce == null)
			{
				Errors = new Error[0];
			}
			else if (responce.Errors == null || responce.Errors.Length == 0)
			{
				Errors = new Error[] { new Error {
					Name = "Status",
					Location = "responce",
					Description = $"Unsuccesfull API responce status ({responce.Status}) without defined errors." }
				};
			}
			else
			{
				Errors = responce.Errors;
			}
		}

		public override string Message {
			get {
				return base.Message + "\r\nwith folowing errors:\r\n" +
					string.Join(";\r\n", Errors.Select(e => $"Error with {e.Name} at {e.Location} ({e.Description})"));
			}
		}
	}
}
