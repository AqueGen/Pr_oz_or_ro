using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Cancellation : BaseCancellation, ICancellation
	{
		public Cancellation()
		{
		}

		public Cancellation(ICancellation cancellation)
			: base(cancellation)
		{
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }

		public int? LotId { get; set; }

		public virtual Lot Lot { get; set; }

		/// <summary>
		/// Супровідна документація скасування: Протокол рішення Тендерного комітету Замовника про скасування закупівлі.
		/// </summary>
		public virtual ICollection<CancellationDocument> Documents { get; set; }
	}
}
