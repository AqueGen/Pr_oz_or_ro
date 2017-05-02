using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IAward : IStringId, ITitled
	{
		/// <summary>
		/// OpenContracting Description: Поточний статус рішення, взятий зі списку кодів awardStatus.
		/// </summary>
		string Status { get; set; }

		/// <summary>
		///  Генерується автоматично, лише для читання.
		///  OpenContracting Description: Дата рішення про підписання договору.
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Загальна вартість згідно цього рішення.
		/// </summary>
		Value Value { get; set; }

		/// <summary>
		/// Період часу, під час якого можна подавати скарги.
		/// </summary>
		Period ComplaintPeriod { get; set; }
	}
}
