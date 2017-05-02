using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Адреса (необов'язкова)
    /// </summary>
    [ComplexType]
    public class AddressOptional : IComplexType, IAddress, IEquatable<AddressOptional>
    {
        public AddressOptional()
        {
        }

        public AddressOptional(IAddress address)
        {
            StreetAddress = address.StreetAddress;
            Locality = address.Locality;
            Region = address.Region;
            PostalCode = address.PostalCode;
            CountryName = address.CountryName;
        }

        /// <summary>
        /// OpenContracting Description: Вулиця. Наприклад, вул.Хрещатик, 22.
        /// </summary>
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// OpenContracting Description: Населений пункт. Наприклад, Київ.
        /// </summary>
        [JsonProperty("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// OpenContracting Description: Область. Наприклад, Київська.
        /// </summary>
        [JsonProperty("region")]
        [StringLength(128), Truncate]
        public string Region { get; set; }

        /// <summary>
        /// OpenContracting Description: Поштовий індекс, Наприклад, 78043.
        /// </summary>
        [JsonProperty("postalCode")]
        [StringLength(128), Truncate]
        public string PostalCode { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Назва країни. Наприклад, Україна.
        /// </summary>
        [JsonRequired]
        [JsonProperty("countryName")]
        [StringLength(128), Truncate]
        public string CountryName { get; set; }

        public bool IsDefined()
        {
            return !string.IsNullOrEmpty(StreetAddress + Locality + Region + PostalCode + CountryName);
        }

        public bool Equals(AddressOptional other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(StreetAddress, other.StreetAddress) && string.Equals(Locality, other.Locality) &&
                   string.Equals(Region, other.Region) && string.Equals(PostalCode, other.PostalCode) &&
                   string.Equals(CountryName, other.CountryName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AddressOptional)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (StreetAddress != null ? StreetAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Locality != null ? Locality.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Region != null ? Region.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CountryName != null ? CountryName.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(CountryName)
                && string.IsNullOrEmpty(PostalCode)
                && string.IsNullOrEmpty(Region)
                && string.IsNullOrEmpty(Locality)
                && string.IsNullOrEmpty(StreetAddress);
        }
    }

    /// <summary>
    /// Адреса. CountryName обов'язково (задано через Fluent API)
    /// </summary>
    [ComplexType]
    public class Address : AddressOptional, IComplexType
    {
        public Address()
        {
        }

        public Address(IAddress address)
            : base(address)
        {
        }
    }
}