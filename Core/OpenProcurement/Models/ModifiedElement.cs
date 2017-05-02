using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Models
{
    public class ModifiedElement : IGuid, IModified
    {
        public Guid Guid { get; set; }

        public DateTime DateModified { get; set; }
    }
}
