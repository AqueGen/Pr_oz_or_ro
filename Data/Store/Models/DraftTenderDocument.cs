using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class DraftTenderDocument : BaseDraftDocument, IDraftDocument
	{
		public DraftTenderDocument()
		{
		}

		public DraftTenderDocument(IDraftDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual DraftTender Tender { get; set; }

		/// <summary>
		/// Id пов’язаних Lot або Item.
		/// </summary>
		public string RelatedId { get; set; }

		public byte[] Data { get; set; }
	}
}
