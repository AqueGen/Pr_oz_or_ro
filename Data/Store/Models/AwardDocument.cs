using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class AwardDocument : BaseDocument, IDocument
	{
		public AwardDocument()
		{
		}

		public AwardDocument(IDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int AwardId { get; set; }

		public virtual Award Award { get; set; }
	}
}
