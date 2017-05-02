using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface ILotValue
	{
		/// <summary>
		/// Обов'язково!
		/// Правила валідації:
		/// - amount повинно бути меншим, ніж Lot.value.amount
		/// - currency повинно або бути відсутнім, або відповідати Lot.value.currency
		/// - valueAddedTaxIncluded повинно або бути відсутнім, або відповідати Lot.value.valueAddedTaxIncluded
		/// </summary>
		Value Value { get; set; }

		/// <summary>
		/// Генерується автоматично
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Веб-адреса для участі в аукціоні.
		/// </summary>
		string ParticipationUrl { get; set; }
	}
}
