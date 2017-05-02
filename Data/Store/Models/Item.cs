using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class Item : BaseItem, IItem
    {
        public Item()
        {
        }

        public Item(IItem item)
            : base(item)
        {
        }

        public int Id { get; set; }

        public ClassificationCPVOptional Classification { get; set; }

        public virtual ICollection<Classification> AdditionalClassifications { get; set; }

        [Required]
        public int TenderId { get; set; }

        public virtual Tender Tender { get; set; }

        public int? LotId { get; set; }

        public virtual Lot Lot { get; set; }

        public int? AwardId { get; set; }

        public virtual Award Award { get; set; }

        public int? ContractId { get; set; }

        public virtual Contract Contract { get; set; }
    }
}