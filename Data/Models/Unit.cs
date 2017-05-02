using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Одиниця
    /// </summary>
    [ComplexType]
    public class Unit : IComplexType, IUnit, IEquatable<Unit>
    {
        /// <summary>
        /// Обов'язково!
        /// Код одиниці в UN/CEFACT Recommendation 20.
        /// </summary>
        [JsonRequired]
        [JsonProperty("code")]
        [StringLength(32), Truncate]
        public string Code { get; set; }

        /// <summary>
        /// OpenContracting Description: Назва одиниці
        /// </summary>
        [JsonProperty("name")]
        [StringLength(128), Truncate]
        public string Name { get; set; }

        public bool Equals(Unit other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Code, other.Code) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Unit)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code != null ? Code.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Code)
                && string.IsNullOrEmpty(Name);
        }
    }
}