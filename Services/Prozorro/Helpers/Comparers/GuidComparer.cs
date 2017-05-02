using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Services.Prozorro.Helpers.Comparers
{
	public class GuidComparer : IEqualityComparer<IGuid>
	{
		public bool Equals(IGuid x, IGuid y)
		{
			return x.Guid.Equals(y.Guid);
		}

		public int GetHashCode(IGuid obj)
		{
			return obj.Guid.GetHashCode();
		}

		public static GuidComparer Instance { get; } = new GuidComparer();
	}
}
