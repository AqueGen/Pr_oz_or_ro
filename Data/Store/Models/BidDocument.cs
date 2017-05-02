using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class BidDocument : BaseDocument, IDocument
	{
		public BidDocument()
		{
		}

		public BidDocument(IDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int BidId { get; set; }

		public virtual Bid Bid { get; set; }
	}
}
