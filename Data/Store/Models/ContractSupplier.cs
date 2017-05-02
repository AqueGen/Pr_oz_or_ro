using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class ContractSupplier : Organization<ContractSupplierIdentifier>
	{
		public ContractSupplier()
		{
		}

		public ContractSupplier(IOrganization organization)
			: base(organization)
		{
		}

		public int ContractId { get; set; }

		public virtual Contract Contract { get; set; }
	}

	public class ContractSupplierIdentifier : Identifier<ContractSupplier>
	{
		public ContractSupplierIdentifier()
		{
		}

		public ContractSupplierIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
