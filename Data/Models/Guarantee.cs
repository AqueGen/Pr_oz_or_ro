using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Забезпечення тендерної пропозиції
    /// </summary>
    [ComplexType]
    public class Guarantee : IComplexType, IGuarantee, IEquatable<Guarantee>
    {
        public Guarantee()
        {
        }

        public Guarantee(IGuarantee guarantee)
        {
            Amount = guarantee.Amount;
            Currency = guarantee.Currency;
        }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Кількість як число.
        /// Повинно бути додатнім.
        /// </summary>
        // [JsonRequired]
        [JsonProperty("amount", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public decimal Amount { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Валюта у трибуквенному форматі ISO 4217.
        /// За замовчуванням = UAH
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty("currency")]
        [StringLength(3), Truncate]
        public string Currency { get; set; }

        public bool Equals(Guarantee other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && string.Equals(Currency, other.Currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Guarantee)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount.GetHashCode() * 397) ^ (Currency != null ? Currency.GetHashCode() : 0);
            }
        }

        public virtual bool IsEmpty()
        {
            return Amount == decimal.Zero
                && string.IsNullOrEmpty(Currency);
        }
    }
}