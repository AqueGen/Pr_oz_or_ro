using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Організація замовник
    /// </summary>
    public class PlanProcuringEntity : BasePlanOrganization, IOrganization, IProcuringEntity
    {
        public PlanProcuringEntity()
        {
        }

        public PlanProcuringEntity(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        /// <summary>
        /// Тип замовника
        /// </summary>
        [JsonProperty("kind")]
        public ProcuringEntityType? Kind { get; set; }

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
