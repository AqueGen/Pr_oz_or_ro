using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IIdentifier
	{
		/// <summary>
		/// OpenContracting Description: Ідентифікатори організації беруться з існуючої схеми ідентифікації. 
		/// Це поле вказує схему або список кодів, де можна знайти ідентифікатор організації. 
		/// Це значення повинно братись зі Схеми Ідентифікації Організацій.
		/// </summary>
		string Scheme { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Ідентифікатор організації у вибраній схемі.
		/// Дозволеними є коди зі спику кодів “Organisation Registration Agency” Стандарту IATI
		/// http://iatistandard.org/codelists/OrganisationRegistrationAgency/
		/// з додаванням коду UA-EDR для організацій, зареєстрованих в Україні(ЄДРПОУ та ІПН).
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// OpenContracting Description: Легально зареєстрована назва організації.
		/// </summary>
		string LegalName { get; set; }

        string LegalNameEn { get; set; }

        /// <summary>
        /// OpenContracting Description: URI для ідентифікації організації, 
        /// наприклад, ті, які надають Open Corporates або інші постачальники URI. 
        /// Це не для вебсайту організації, його можна надати в полі url в ContactPoint організації.
        /// </summary>
        string Uri { get; set; }
	}
}
