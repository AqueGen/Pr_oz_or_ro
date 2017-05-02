using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Лот закупівлі
    /// </summary>
    public class Lot : BaseLot
    {
        public Lot()
        {
        }

        public Lot(IDraftLot lot)
            : base(lot)
        {
        }

        public Lot(ILot lot)
            : base(lot)
        {
        }
    }
}
