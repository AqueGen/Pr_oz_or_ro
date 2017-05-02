using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class TenderComplaintAuthor : Organization<TenderComplaintAuthorIdentifier>
	{
		public TenderComplaintAuthor()
		{
		}

		public TenderComplaintAuthor(IOrganization organization)
			: base(organization)
		{
		}

		public virtual TenderComplaint Complaint { get; set; }
	}

	public class TenderComplaintAuthorIdentifier : Identifier<TenderComplaintAuthor>
	{
		public TenderComplaintAuthorIdentifier()
		{
		}

		public TenderComplaintAuthorIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
