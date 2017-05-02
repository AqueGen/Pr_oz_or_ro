using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Bid : BaseBid, IBid
	{
		public Bid()
		{
		}

		public Bid(IBid bid)
			: base(bid)
		{
			if (Value == null)
				Value = new Value();
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

        public virtual Tender Tender { get; set; }

        /// <summary>
        /// Список організацій
        /// </summary>
        public virtual ICollection<Tenderer> Tenderers { get; set; }

        /// <summary>
        /// Документи
        /// </summary>
        public virtual ICollection<BidDocument> Documents { get; set; }

        /// <summary>
        /// Критерії ставки
        /// </summary>
        public virtual ICollection<Parameter> Parameters { get; set; }

        /// <summary>
        /// LotValues
        /// </summary>
        public virtual ICollection<LotValue> LotValues { get; set; }
    }
}
