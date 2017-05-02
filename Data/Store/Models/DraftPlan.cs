using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class DraftPlan : BaseDraftPlan, IDraftPlan
    {
        public DraftPlan()
        {
        }

        public DraftPlan(IDraftPlan plan)
            : base(plan)
        {
            // EF ComplexType objects cannot be null
            // MinimalStep is required but sometimes prozoro returns null
            if (Classification == null)
                Classification = new ClassificationCPVOptional();
            if (Budget == null)
                Budget = new Budget();
            if (Budget.Project == null)
                Budget.Project = new Project();
            if (Tender == null)
                Tender = new PlanTender();
            if (Tender.TenderPeriod == null)
                Tender.TenderPeriod = new Period();
        }

        public int Id { get; set; }

        // Concurency resolving timestamp
        // EF includes a property in the where clause, during the update operation, if the property is marked with the Timestamp attribute.
        // http://www.entityframeworktutorial.net/EntityFramework5/handle-concurrency-in-entity-framework.aspx
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int ProcuringEntityId { get; set; }

        public virtual UserOrganization ProcuringEntity { get; set; }

        public ClassificationCPVOptional Classification { get; set; }

        public virtual ICollection<DraftPlanClassification> AdditionalClassifications { get; set; }

        public virtual ICollection<DraftPlanItem> Items { get; set; }    
    }
}
