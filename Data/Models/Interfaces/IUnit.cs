using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IUnit
	{
		/// <summary>
		/// Обов'язково!
		/// Код одиниці в UN/CEFACT Recommendation 20.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// OpenContracting Description: Назва одиниці
		/// </summary>
		string Name { get; set; }
	}
}
