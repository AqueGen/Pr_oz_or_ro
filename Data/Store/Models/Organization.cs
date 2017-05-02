using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Organization<TIdentifier> : BaseOrganization, IOrganization
		where TIdentifier : class, IOrganizationIdentifier
	{
		public Organization()
		{
		}

		public Organization(IOrganization organization)
			: base(organization)
		{
			if (Address == null)
				Address = new Address();
			if (ContactPoint == null)
				ContactPoint = new ContactPoint();
		}

		public int Id { get; set; }

		public virtual ICollection<TIdentifier> AllIdentifiers { get; set; }

		[NotMapped]
		public TIdentifier Identifier {
			get {
				return AllIdentifiers?.FirstOrDefault(i => i.Main);
			}
		}

		[NotMapped]
		public IEnumerable<TIdentifier> AdditionalIdentifiers {
			get {
				IOrganizationIdentifier main = Identifier;
				return AllIdentifiers?.Where(i => i != main);
			}
		}
	}
}
