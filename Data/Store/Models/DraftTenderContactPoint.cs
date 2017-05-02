using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Store.Models
{
    public class DraftTenderContactPoint
    {
        [Key, Column(Order = 0)]
        public int TenderId { get; set; }

        [Key, Column(Order = 1)]
        public int ContactPointId { get; set; }

        public int SortingOrder { get; set; }

        public virtual DraftTender Tender { get; set; }

        public virtual UserOrganizationContactPoint ContactPoint { get; set; }
    }
}
