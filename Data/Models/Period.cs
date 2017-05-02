using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Значення startDate завжди повинно йти перед endDate.
    /// Дата/час у Формат дати: ISO 8601.
    /// </summary>
    [ComplexType]
    public class Period : IComplexType, IEquatable<Period>
    {
        public Period()
        {
        }

        public Period(DateTime? startDate) : this(startDate, null)
        {
        }

        public Period(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Period(Period period)
        {
            StartDate = period.StartDate;
            EndDate = period.EndDate;
        }

        /// <summary>
        /// OpenContracting Description: Початкова дата періоду.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// OpenContracting Description: Кінцева дата періоду.
        /// </summary>
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }

        public bool Equals(Period other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StartDate.Equals(other.StartDate) && EndDate.Equals(other.EndDate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Period)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (StartDate.GetHashCode() * 397) ^ EndDate.GetHashCode();
            }
        }

        public virtual bool IsEmpty()
        {
            return !StartDate.HasValue
                && !EndDate.HasValue;
        }
    }
}