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
	/// <summary>
	/// Географічні координати місця доставки. 
	/// </summary>
	public class DeliveryLocation : IComplexType, IDeliveryLocation, IEquatable<DeliveryLocation>
	{
		/// <summary>
		/// Обов'язково!
		/// latitude
		/// </summary>
		[JsonRequired]
		[JsonProperty("latitude")]
		[StringLength(32), Truncate]
		public string Latitude { get; set; }

		/// <summary>
		/// Обов'язково!
		/// longitude
		/// </summary>
		[JsonRequired]
		[JsonProperty("longitude")]
		[StringLength(32), Truncate]
		public string Longitude { get; set; }

		/// <summary>
		/// elevation переважно не використовується
		/// </summary>
		[JsonProperty("elevation")]
		[StringLength(32), Truncate]
		public string Elevation { get; set; }

	    public bool Equals(DeliveryLocation other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return string.Equals(Latitude, other.Latitude) && string.Equals(Longitude, other.Longitude) && string.Equals(Elevation, other.Elevation);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((DeliveryLocation) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = (Latitude != null ? Latitude.GetHashCode() : 0);
	            hashCode = (hashCode*397) ^ (Longitude != null ? Longitude.GetHashCode() : 0);
	            hashCode = (hashCode*397) ^ (Elevation != null ? Elevation.GetHashCode() : 0);
	            return hashCode;
	        }
	    }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Latitude)
                && string.IsNullOrEmpty(Longitude)
                && string.IsNullOrEmpty(Elevation);
        }
    }
}
