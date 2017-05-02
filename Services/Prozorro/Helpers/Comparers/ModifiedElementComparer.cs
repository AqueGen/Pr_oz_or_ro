using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Helpers.Comparers
{
    public class ModifiedElementComparer : IEqualityComparer<ModifiedElement>
    {
        public bool Equals(ModifiedElement x, ModifiedElement y)
        {
            return x.Guid == y.Guid && x.DateModified == y.DateModified;
        }

        public int GetHashCode(ModifiedElement obj)
        {
            return obj.DateModified.GetHashCode();
        }

        public static ModifiedElementComparer Instance { get; } = new ModifiedElementComparer();
    }
}
