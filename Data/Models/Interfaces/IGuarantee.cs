using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IGuarantee
	{
		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Кількість як число.
		/// Повинно бути додатнім.
		/// </summary>
		decimal Amount { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Валюта у трибуквенному форматі ISO 4217.
		/// За замовчуванням = UAH
		/// </summary>
		string Currency { get; set; }
	}
}
