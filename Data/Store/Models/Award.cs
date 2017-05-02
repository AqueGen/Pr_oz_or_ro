using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Award : BaseAward, IAward
	{
		public Award()
		{
		}

		public Award(IAward award)
			: base(award)
		{
			if (Value == null)
				Value = new Value();
			if (ComplaintPeriod == null)
				ComplaintPeriod = new Period();
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set;}

		public int? BidId { get; set; }

		public virtual Bid Bid { get; set; }

		public int? LotId { get; set; }

		public virtual Lot Lot { get; set; }


		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Постачальники, що були визнані переможцями згідно цього рішення.
		/// </summary>
		public virtual ICollection <Supplier> Suppliers { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Товари та послуги, що розглядались цим рішенням, 
		/// поділені на окремі рядки, де це можливо. 
		/// Елементи не повинні бути продубльовані, а повинні мати вказану кількість.
		/// </summary>
		public virtual ICollection<Item> Items { get; set; }

		/// <summary>
		/// OpenContracting Description: Усі документи та додатки пов’язані з рішенням, включно з будь-якими повідомленнями.
		/// </summary>
		public virtual ICollection<AwardDocument> Documents { get; set; }

		/// <summary>
		/// Список скарг.
		/// </summary>
		public virtual ICollection<AwardComplaint> Complaints { get; set; }
	}
}
