using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Об’єкт Cancellation описує причину скасування закупівлі та надає відповідні документи, якщо такі є.
	/// </summary>
	public abstract class BaseCancellation : ICancellation
	{
		public BaseCancellation()
		{
		}

		public BaseCancellation(ICancellation cancellation)
		{
			StringId = cancellation.StringId;
			Reason = cancellation.Reason;
			Status = cancellation.Status;
			Date = cancellation.Date;
			CancellationType = cancellation.CancellationType;
		}

        /// <summary>
        /// uid, генерується автоматично
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Причина, з якої скасовується закупівля.
		/// </summary>
		[JsonProperty("reason")]
		public string Reason { get; set; }

		/// <summary>
		/// Статус скасування
		/// </summary>
		[JsonProperty("status")]
		[StringLength(32), Truncate]
		public string Status { get; set; }

		/// <summary>
		/// Дата скасування
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Тип скасування.
		/// </summary>
		[JsonProperty("cancellationOf")]
		public CancellationType CancellationType { get; set; }
	}
}
