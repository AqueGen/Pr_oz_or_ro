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
	/// <summary>
	/// LotValue
	/// </summary>
	public abstract class BaseLotValue : ILotValue
	{
		public BaseLotValue()
		{
		}

		public BaseLotValue(ILotValue lotValue)
		{
			Value = lotValue.Value;
			Date = lotValue.Date;
			ParticipationUrl = lotValue.ParticipationUrl;
		}

		/// <summary>
		/// Обов'язково!
		/// Правила валідації:
		/// - amount повинно бути меншим, ніж Lot.value.amount
		/// - currency повинно або бути відсутнім, або відповідати Lot.value.currency
		/// - valueAddedTaxIncluded повинно або бути відсутнім, або відповідати Lot.value.valueAddedTaxIncluded
		/// </summary>
		[Required]
		[JsonRequired]
		[JsonProperty("value")]
		public Value Value { get; set; }

		/// <summary>
		/// Генерується автоматично
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Веб-адреса для участі в аукціоні.
		/// </summary>
		[JsonProperty("participationUrl")]
		public string ParticipationUrl { get; set; }
	}
}
