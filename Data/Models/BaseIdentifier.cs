using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
	public class BaseIdentifier : IIdentifier
	{
		public BaseIdentifier()
		{
		}

		public BaseIdentifier(IIdentifier identifier)
		{
			Scheme = identifier.Scheme;
			Id = identifier.Id;
			LegalName = identifier.LegalName;
            LegalNameEn = identifier.LegalNameEn;

            Uri = identifier.Uri;
		}

		/// <summary>
		/// OpenContracting Description: Ідентифікатори організації беруться з існуючої схеми ідентифікації. 
		/// Це поле вказує схему або список кодів, де можна знайти ідентифікатор організації. 
		/// Це значення повинно братись зі Схеми Ідентифікації Організацій.
		/// </summary
		[StringLength(32)]
		[JsonProperty("scheme")]
		public string Scheme { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Ідентифікатор організації у вибраній схемі.
		/// Дозволеними є коди зі спику кодів “Organisation Registration Agency” Стандарту IATI
		/// http://iatistandard.org/codelists/OrganisationRegistrationAgency/
		/// з додаванням коду UA-EDR для організацій, зареєстрованих в Україні(ЄДРПОУ та ІПН).
		/// </summary>
		[Required(AllowEmptyStrings = true)]
		[JsonRequired]
		[StringLength(32)]
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// OpenContracting Description: Легально зареєстрована назва організації.
		/// </summary>
		[JsonProperty("legalName")]
		public string LegalName { get; set; }

		[JsonProperty("legalName_en")]
        public string LegalNameEn { get; set; }

        /// <summary>
        /// OpenContracting Description: URI для ідентифікації організації, 
        /// наприклад, ті, які надають Open Corporates або інші постачальники URI. 
        /// Це не для вебсайту організації, його можна надати в полі url в ContactPoint організації.
        /// </summary>
        [JsonProperty("uri")]
		public string Uri { get; set; }
	}
}
