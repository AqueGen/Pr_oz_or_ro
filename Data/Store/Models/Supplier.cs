using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Supplier : Organization<SupplierIdentifier>
	{
		public Supplier()
		{
		}

		public Supplier(IOrganization organization)
			: base(organization)
		{
		}

		public int AwardId { get; set; }

		public virtual Award Award { get; set; }
	}

	public class SupplierIdentifier : Identifier<Supplier>
	{
		public SupplierIdentifier()
		{
		}

		public SupplierIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
