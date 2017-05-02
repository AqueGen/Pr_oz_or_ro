using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class CancellationDocument : BaseDocument, IDocument
	{
		public CancellationDocument()
		{
		}

		public CancellationDocument(IDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int CancellationId { get; set; }

		public virtual Cancellation Cancellation { get; set; }
	}
}
