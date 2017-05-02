using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Організація замовник
    /// </summary>
    public class ProcuringEntity : Organization, IProcuringEntity
    {
        public ProcuringEntity()
        {
        }

        public ProcuringEntity(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        /// <summary>
        /// Тип замовника
        /// </summary>
        [JsonProperty("kind")]
        public ProcuringEntityType? Kind { get; set; }

        [JsonProperty("additionalContactPoints")]
        public ContactPoint[] AdditionalContactPoints { get; set; }

        /// <summary>
        /// Об'єднана послідовність головного і додаткових контактів.
        /// </summary>
        [JsonIgnore]
        public IEnumerable<ContactPoint> ContactPoints
        {
            get {
                if (ContactPoint != null)
                    yield return ContactPoint;
                if (AdditionalContactPoints != null)
                    foreach (ContactPoint additional in AdditionalContactPoints)
                        yield return additional;
            }
            set {
                ContactPoint = value?.FirstOrDefault();
                AdditionalContactPoints = value?.Skip(1).ToArray();
            }
        }
    }
}