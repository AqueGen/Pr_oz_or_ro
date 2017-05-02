using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class DraftLot : BaseDraftLot, IDraftLot
	{
		public DraftLot()
		{
		}

		public DraftLot(IDraftLot lot)
			: base(lot)
		{
		}

		public int Id { get; set; }

		/// <summary>
		/// Забезпечення тендерної пропозиції
		/// </summary>
		internal GuaranteeOptional GuaranteeOptional {
			get { return Guarantee; }
			set { Guarantee = value; }
		}

		[Required]
		public int TenderId { get; set; }

		public virtual DraftTender Tender { get; set; }

		public virtual ICollection<DraftItem> Items { get; set; }

	    public Action<DraftLot, EntityState> LotChangeSubscriber;
	}
}
