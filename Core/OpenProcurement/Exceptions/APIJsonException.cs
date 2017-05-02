using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	public class APIJsonException : APIException
	{
		public string Content { get; }
		internal APIJsonException(Type type, Exception innerException, string content)
			: base(type.FullName + " cannot be parsed.", innerException)
		{
			Content = content;
		}

		public override string ToString()
		{
			return base.ToString() + "\r\n" + Content;
		}
	}
}
