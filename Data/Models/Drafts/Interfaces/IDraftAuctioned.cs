using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
	public interface IDraftAuctioned
	{
		/// <summary>
		/// Обов'язково!
		/// Повний доступний бюджет закупівлі чи лоту. Пропозиції, що більші за value будуть відхилені.
		/// OpenContracting Description: Загальна кошторисна вартість закупівлі чи лоту.
		/// </summary>
		Value Value { get; set; }

		/// <summary>
		/// Забезпечення тендерної пропозиції чи лоту
		/// </summary>
		Guarantee Guarantee { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Мінімальний крок аукціону (редукціону). Правила валідації:
		/// Значення amount повинно бути меншим за value.amount
		/// Значення currency повинно бути або відсутнім, або співпадати з value.currency
		/// Значення valueAddedTaxIncluded повинно бути або відсутнім, або співпадати з value.valueAddedTaxIncluded
		/// </summary>
		Value MinimalStep { get; set; }
	}
}
