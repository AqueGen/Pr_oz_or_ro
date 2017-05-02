using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Services.Prozorro.Helpers.Comparers
{
	public class StringIdComparer : IEqualityComparer<IStringId>
    {
        public bool Equals(IStringId x, IStringId y)
        {
            return x.StringId.Equals(y.StringId, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(IStringId obj)
        {
            return obj.StringId.GetHashCode();
        }

        public static StringIdComparer Instance { get; } = new StringIdComparer();
    }
}
