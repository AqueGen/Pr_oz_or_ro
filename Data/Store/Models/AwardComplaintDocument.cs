using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class AwardComplaintDocument : BaseDocument, IDocument
	{
		public AwardComplaintDocument()
		{
		}

		public AwardComplaintDocument(IDocument document)
			: base(document)
		{
		}

		public int Id { get; set; }

		public int ComplaintId { get; set; }

		public virtual AwardComplaint Complaint { get; set; }
	}
}
