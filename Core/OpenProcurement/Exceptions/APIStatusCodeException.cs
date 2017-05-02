using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	public class APIStatusCodeException : APIException
	{
		public string ReasonPhrase { get; }
		public HttpStatusCode StatusCode { get; }

		internal APIStatusCodeException(HttpStatusCode statusCode, string reasonPhrase)
		{
			StatusCode = statusCode;
			ReasonPhrase = reasonPhrase;
		}

		public override string Message {
			get {
				return $"OpenProcurement API returned {StatusCode}: {ReasonPhrase}";
			}
		}
	}
}
