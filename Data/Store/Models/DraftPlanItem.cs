using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
    public class DraftPlanItem : BaseDraftItem, IDraftItem
    {
        public DraftPlanItem()
        {
        }

        public DraftPlanItem(IItem item)
            : base(item)
        {
            // EF ComplexType objects cannot be null
            if (Unit == null)
                Unit = new Unit();
            if (DeliveryDate == null)
                DeliveryDate = new Period();
            if (DeliveryAddress == null)
                DeliveryAddress = new AddressOptional();
            if (DeliveryLocation == null)
                DeliveryLocation = new DeliveryLocation();
            if (Classification == null)
                Classification = new ClassificationCPVOptional();
        }

        public DraftPlanItem(IDraftItem item)
            : base(item)
        {
            // EF ComplexType objects cannot be null
            if (Unit == null)
                Unit = new Unit();
            if (DeliveryDate == null)
                DeliveryDate = new Period();
            if (DeliveryAddress == null)
                DeliveryAddress = new AddressOptional();
            if (DeliveryLocation == null)
                DeliveryLocation = new DeliveryLocation();
            if (Classification == null)
                Classification = new ClassificationCPVOptional();
        }

        

        public int Id { get; set; }

        public ClassificationCPVOptional Classification { get; set; }

        public virtual ICollection<DraftPlanItemClassification> AdditionalClassifications { get; set; }

        [Required]
        public int PlanId { get; set; }

        public virtual DraftPlan Plan { get; set; }
    }
}
