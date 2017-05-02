using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Tenderer : Organization<TendererIdentifier>
	{
		public Tenderer()
		{
		}

		public Tenderer(IOrganization organization)
			: base(organization)
		{
		}

		public int BidId { get; set; }

		public virtual Bid Bid { get; set; }
	}

	public class TendererIdentifier : Identifier<Tenderer>
	{
		public TendererIdentifier()
		{
		}

		public TendererIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
