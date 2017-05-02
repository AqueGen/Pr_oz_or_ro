using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
    public abstract class BaseOrganization : IOrganization
    {
        public BaseOrganization()
        {
        }

        public BaseOrganization(IOrganization organization)
        {
            Name = organization.Name;
            NameEn = organization.NameEn;
            Address = organization.Address;
            ContactPoint = organization.ContactPoint;
        }

        /// <summary>
        /// OpenContracting Description: Назва організації.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_en")]
        public string NameEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Адреса організації
        /// </summary>
        [JsonRequired]
        [JsonProperty("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Контактна особа організації
        /// </summary>
        [JsonRequired]
        [JsonProperty("contactPoint")]
        public virtual ContactPoint ContactPoint { get; set; }
    }
}