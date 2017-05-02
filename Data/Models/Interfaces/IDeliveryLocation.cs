using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IDeliveryLocation
	{
		/// <summary>
		/// Обов'язково!
		/// latitude
		/// </summary>
		string Latitude { get; set; }

		/// <summary>
		/// Обов'язково!
		/// longitude
		/// </summary>
		string Longitude { get; set; }

		/// <summary>
		/// elevation переважно не використовується
		/// </summary>
		string Elevation { get; set; }
	}
}
