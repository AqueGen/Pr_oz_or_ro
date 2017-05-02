using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IContract : IStringId, ITitled
	{
		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// </summary>
		string Identifier { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Award.id вказує на рішення, згідно якого видається договір.
		/// </summary>
		string ContractNumber { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Загальна вартість договору.
		/// </summary>
		Value Value { get; set; }

		/// <summary>
		/// OpenContracting Description: Поточний статус договору.
		/// </summary>
		string Status { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата початку та завершення договору.
		/// </summary>
		Period Period { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата підписання договору. 
		/// Якщо було декілька підписань, то береться дата останнього підписання.
		/// </summary>
		DateTime? DateSigned { get; set; }
	}
}
