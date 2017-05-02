using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class AwardComplaintAuthor : Organization<AwardComplaintAuthorIdentifier>
	{
		public AwardComplaintAuthor()
		{
		}

		public AwardComplaintAuthor(IOrganization organization)
			: base(organization)
		{
		}

		public virtual AwardComplaint Complaint { get; set; }
	}

	public class AwardComplaintAuthorIdentifier : Identifier<AwardComplaintAuthor>
	{
		public AwardComplaintAuthorIdentifier()
		{
		}

		public AwardComplaintAuthorIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
