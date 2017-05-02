using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class DraftItem : BaseDraftItem, IDraftItem
    {
        public DraftItem()
        {
        }

        public DraftItem(IDraftItem item)
            : base(item)
        {
        }

        public int Id { get; set; }

        public ClassificationCPVOptional Classification { get; set; }

        public virtual ICollection<DraftClassification> AdditionalClassifications { get; set; }

        [Required]
        public int TenderId { get; set; }

        public virtual DraftTender Tender { get; set; }

        public int? LotId { get; set; }

        public virtual DraftLot Lot { get; set; }
    }
}