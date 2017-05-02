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
    public class DraftPlanItemClassification : BaseClassification, IClassification
    {
        public DraftPlanItemClassification()
        {
        }

        public DraftPlanItemClassification(BaseClassification classification)
            : base(classification)
        {
        }

        [Key]
        public int InternalId { get; set; }

        [Required]
        public int ItemId { get; set; }

        public virtual DraftPlanItem Item { get; set; }
    }
}
