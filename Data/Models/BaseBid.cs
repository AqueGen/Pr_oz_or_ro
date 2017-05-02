using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Пропозиція
	/// </summary>
	public abstract class BaseBid : IBid
	{
		public BaseBid()
		{
		}

		public BaseBid(IBid bid)
		{
			StringId = bid.StringId;
			Date = bid.Date;
			Status = bid.Status;
			Value = bid.Value;
			ParticipationUrl = bid.ParticipationUrl;
		}

        /// <summary>
        /// uid, генерується автоматично
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Генерується автоматично
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Статус ставки
		/// </summary>
		[JsonProperty("status")]
		[StringLength(32), Truncate]
		public string Status { get; set; }

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
		[JsonProperty("value")]
		public Value Value { get; set; }

		/// <summary>
		/// Веб-адреса для участі в аукціоні.
		/// </summary>
		[JsonProperty("participationUrl")]
		public string ParticipationUrl { get; set; }
	}
}
