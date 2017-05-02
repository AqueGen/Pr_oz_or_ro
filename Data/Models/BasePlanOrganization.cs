using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
    public abstract class BasePlanOrganization : IOrganization
    {
        public BasePlanOrganization()
        {
        }

        public BasePlanOrganization(IOrganization organization)
        {
            Name = organization.Name;
            Address = organization.Address;
            ContactPoint = organization.ContactPoint;
        }

        /// <summary>
        /// OpenContracting Description: Назва організації.
        /// </summary>
        [StringLength(256), Truncate]
        [JsonProperty("name")]
        public string Name { get; set; }

        [StringLength(256), Truncate]
        [JsonProperty("name_en")]
        public string NameEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Адреса організації
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Контактна особа організації
        /// </summary>
        [JsonProperty("contactPoint")]
        public ContactPoint ContactPoint { get; set; }
    }
}
