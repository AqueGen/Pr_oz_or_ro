using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models.Drafts
{
    public class BaseDraftPlan : IGuid, IModified, IDraftPlan
    {
        public BaseDraftPlan()
        {
        }

        public BaseDraftPlan(IDraftPlan plan)
        {
            Guid = plan.Guid;
            DateModified = plan.DateModified;
            Budget = plan.Budget;
            Tender = plan.Tender;
        }

        public Guid Guid { get; set; }

        public DateTime DateModified { get; set; }

        [JsonRequired]
        [JsonProperty("budget")]
        public Budget Budget { get; set; }

        [JsonRequired]
        [JsonProperty("tender")]
        public PlanTender Tender { get; set; }
    }
}
