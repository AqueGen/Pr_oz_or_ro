using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IBid : IStringId
    {
		/// <summary>
		/// Генерується автоматично
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Статус ставки
		/// </summary>
		string Status { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Правила валідації:
		/// Значення amount повинно бути меншим за Tender.value.amout
		/// Значення currency повинно бути або відсутнім, або співпадати з Tender.value.currency
		/// Значення valueAddedTaxIncluded повинно бути або відсутнім, або співпадати з Tender.value.valueAddedTaxIncluded
		/// </summary>
		// TOTO Taras 5: recheck if this field is required
		// Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
		//[JsonRequired]
		Value Value { get; set; }

		/// <summary>
		/// Веб-адреса для участі в аукціоні.
		/// </summary>
		string ParticipationUrl { get; set; }
	}
}
