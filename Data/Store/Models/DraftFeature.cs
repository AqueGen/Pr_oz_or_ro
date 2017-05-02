using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class DraftFeature : BaseFeature, IFeature
    {
        public DraftFeature()
        {
        }

        public DraftFeature(IFeature feature)
            : base(feature)
        {
        }

        public int Id { get; set; }

        [Required]
        public int TenderId { get; set; }

        public virtual DraftTender Tender { get; set; }

        public virtual ICollection<DraftFeatureValue> Values { get; set; }
    }
}