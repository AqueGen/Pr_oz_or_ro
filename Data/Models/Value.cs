using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Ціна
    /// </summary>
    [ComplexType]
    public class Value : Guarantee, IComplexType, IValue, IEquatable<Value>
    {
        public Value()
        {
            // Tender.MinimalStep is required but sometimes prozoro returns null
            Currency = "UAH";
        }

        public Value(IValue value)
            : base(value)
        {
            VATIncluded = value.VATIncluded;
        }

        /// <summary>
        /// Обов'язково!
        /// Включено податок на додану вартість?
        /// </summary>
        [JsonRequired]
        [JsonProperty("valueAddedTaxIncluded")]
        public bool VATIncluded { get; set; }

        public bool Equals(Value other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return VATIncluded == other.VATIncluded && base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Value)obj) && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return VATIncluded.GetHashCode() + base.GetHashCode();
        }

        public override bool IsEmpty()
        {
            return base.IsEmpty()
                && !VATIncluded;
        }
    }
}