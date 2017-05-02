using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Attributes
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
	public class TruncateAttribute : Attribute
	{
	}

	public static class StringHelper
	{
		public static string Truncate(this string text, int maxLength)
		{
			return text == null || text.Length <= maxLength
				? text
				: text.Substring(0, maxLength);
		}
	}
}
