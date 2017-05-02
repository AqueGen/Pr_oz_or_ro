using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
	public class Organization : BaseOrganization, IOrganization
	{
		public Organization()
		{
		}

		public Organization(IOrganization organization)
			: base(organization)
		{
		}

		/// <summary>
		/// OpenContracting Description: Ідентифікатор цієї організації.
		/// </summary>
		[JsonProperty("identifier")]
		public Identifier Identifier { get; set; }

		/// <summary>
		/// Список об’єктів Identifier
		/// </summary>
		[JsonProperty("additionalIdentifiers")]
		public Identifier[] AdditionalIdentifiers { get; set; }
	}
}
