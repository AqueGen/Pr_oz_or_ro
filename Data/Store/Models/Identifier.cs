using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public interface IOrganizationIdentifier : IIdentifier
	{
		bool Main { get; set; }
	}

	public class Identifier<TOrganization> : BaseIdentifier, IOrganizationIdentifier
		where TOrganization : IOrganization
	{
		public Identifier()
		{
		}

		public Identifier(IIdentifier identifier)
			: base (identifier)
		{
		}

		[Key]
		public int InternalId { get; set; }

		public int OrganizationId { get; set; }

		public bool Main { get; set; }

		public virtual TOrganization Organization { get; set; }
	}
}
