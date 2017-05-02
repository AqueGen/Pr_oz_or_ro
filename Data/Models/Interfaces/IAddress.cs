using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IAddress
	{
		/// <summary>
		/// OpenContracting Description: Вулиця. Наприклад, вул.Хрещатик, 22.
		/// </summary>
		string StreetAddress { get; set; }

		/// <summary>
		/// OpenContracting Description: Населений пункт. Наприклад, Київ.
		/// </summary>
		string Locality { get; set; }

		/// <summary>
		/// OpenContracting Description: Область. Наприклад, Київська.
		/// </summary>
		string Region { get; set; }

		/// <summary>
		/// OpenContracting Description: Поштовий індекс, Наприклад, 78043.
		/// </summary>
		string PostalCode { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Назва країни. Наприклад, Україна.
		/// </summary>
		string CountryName { get; set; }
	}
}
