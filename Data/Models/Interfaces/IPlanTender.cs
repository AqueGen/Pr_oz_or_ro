using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface IPlanTender
    {
        string Status { get; set; }

        string ProcurementMethod { get; set; }

        string ProcurementMethodRationale { get; set; }

        string ProcurementMethodType { get; set; }

        Period TenderPeriod { get; set; }
    }
}
