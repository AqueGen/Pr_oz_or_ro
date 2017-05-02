using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class Lot : BaseLot, ILot
    {
        public Lot()
        {
        }

        public Lot(ILot lot)
            : base(lot)
        {
        }

        public int Id { get; set; }

        /// <summary>
        /// Забезпечення тендерної пропозиції
        /// </summary>
        internal GuaranteeOptional GuaranteeOptional
        {
            get { return Guarantee; }
            set { Guarantee = value; }
        }

        [Required]
        public int TenderId { get; set; }

        public virtual Tender Tender { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<TenderComplaint> Complaints { get; set; }
    }
}