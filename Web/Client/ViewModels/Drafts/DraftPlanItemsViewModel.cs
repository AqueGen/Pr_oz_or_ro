using System;
using System.Collections.Generic;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftPlanItemsViewModel
    {
        public Guid PlanGuid { get; set; }

        public IEnumerable<DraftPlanItemViewModel> Items { get; set; }
    }
}