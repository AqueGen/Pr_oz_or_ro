using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class TenderDocument : BaseDocument, IDocument
	{
		public TenderDocument()
		{
		}

		public TenderDocument(IDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }
	}
}
