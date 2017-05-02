using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
    public interface IDraftPlan : IGuid, IModified
    {
        Budget Budget { get; set; }

        PlanTender Tender { get; set; }
    }
}
