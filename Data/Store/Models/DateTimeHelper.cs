using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store.Models
{
	public static class DateTimeHelper
	{
		public static DateTime AsUtc(this DateTime value)
		{
			return DateTime.SpecifyKind(value, DateTimeKind.Utc);
		}

		public static DateTime? AsUtc(this DateTime? value)
		{
			return value.HasValue ? value.Value.AsUtc() : value;
		}
	}
}
