using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	public abstract class APIException : Exception
	{
		protected APIException()
		{
		}

		protected APIException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
