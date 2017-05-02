using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
    public class PlanItem : BaseItem, IItem
    {
        public PlanItem()
        {
        }

        public PlanItem(IItem item)
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

        public virtual ICollection<PlanItemClassification> AdditionalClassifications { get; set; }

        [Required]
        public int PlanId { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
