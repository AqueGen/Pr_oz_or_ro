using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class AwardComplaint : BaseComplaint, IComplaint
	{
		public AwardComplaint()
		{
		}

		public AwardComplaint(IComplaint complaint)
			: base(complaint)
		{
		}

		public int Id { get; set; }

		public int AwardId { get; set; }

		public virtual Award Award { get; set; }

		public int? LotId { get; set; }

		public virtual Lot Lot { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Організація, яка подає скаргу (contactPoint - людина, identification - організація, яку ця людина представляє).
		/// </summary>
		// TOTO Taras 5: recheck if this field is required
		// Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
		//[JsonRequired]
		public virtual AwardComplaintAuthor Author { get; set; }

		/// <summary>
		/// Супровідна документація скарги.
		/// </summary>
		public virtual ICollection<AwardComplaintDocument> Documents { get; set; }
	}
}
