using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	[Table("Units")]
	public class StandardUnit : IUnit
	{
		/// <summary>
		/// Обов'язково!
		/// Код одиниці в UN/CEFACT Recommendation 20.
		/// </summary>
		[Key]
		[StringLength(3)]
		public string Code { get; set; }

		/// <summary>
		/// OpenContracting Description: Назва одиниці
		/// </summary>
		[StringLength(128)]
		public string Name { get; set; }
	}
}
