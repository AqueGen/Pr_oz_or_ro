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
	/// Скарга
	/// </summary>
	public abstract class BaseComplaint : IComplaint
	{
		public BaseComplaint()
		{
		}

		public BaseComplaint(IComplaint complaint)
		{
			StringId = complaint.StringId;
			Title = complaint.Title;
			Description = complaint.Description;
			Date = complaint.Date;
			DateSubmitted = complaint.DateSubmitted;
			DateAnswered = complaint.DateAnswered;
			DateEscalated = complaint.DateEscalated;
			DateDecision = complaint.DateDecision;
			DateCanceled = complaint.DateCanceled;
			Status = complaint.Status;
			Type = complaint.Type;
			Resolution = complaint.Resolution;
			ResolutionType = complaint.ResolutionType;
			Satisfied = complaint.Satisfied;
			Decision = complaint.Decision;
			CancellationReason = complaint.CancellationReason;
			TendererAction = complaint.TendererAction;
			TendererActionDate = complaint.TendererActionDate;
		}

        /// <summary>
        /// uid, генерується автоматично
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Заголовок скарги.
		/// </summary>
		[Required]
		[JsonRequired]
		[JsonProperty("title")]
		public string Title { get; set; }

		/// <summary>
		/// Опис запитання.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата подання.
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата, коли вимога була подана.
		/// </summary>
		[JsonProperty("dateSubmitted")]
		public DateTime? DateSubmitted { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата, коли замовник відповів на вимогу.
		/// </summary>
		[JsonProperty("dateAnswered")]
		public DateTime? DateAnswered { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата ескалації (перетворення вимоги на скаргу).
		/// </summary>
		[JsonProperty("dateEscalated")]
		public DateTime? DateEscalated { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// День прийняття рішення по вимозі.
		/// </summary>
		[JsonProperty("dateDecision")]
		public DateTime? DateDecision { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата відхилення.
		/// </summary>
		[JsonProperty("dateCanceled")]
		public DateTime? DateCanceled { get; set; }

		/// <summary>
		/// Статус скарги
		/// </summary>
		[JsonProperty("status")]
		[StringLength(32), Truncate]
		public string Status { get; set; }

		/// <summary>
		/// Тип скарги
		/// </summary>
		[JsonProperty("type")]
		public ComplaintType Type { get; set; }

		/// <summary>
		/// Рішення замовника.
		/// </summary>
		[JsonProperty("resolution")]
		public string Resolution { get; set; }

		/// <summary>
		/// Тип вирішення
		/// </summary>
		[JsonProperty("resolutionType")]
		public ResolutionType ResolutionType { get; set; }

		/// <summary>
		/// Вимога задовільнена?
		/// </summary>
		[JsonProperty("satisfied")]
		public bool Satisfied { get; set; }

		/// <summary>
		/// Рішення органу оскарження.
		/// </summary>
		[JsonProperty("decision")]
		public string Decision { get; set; }

		/// <summary>
		/// Причини відхилення.
		/// </summary>
		[JsonProperty("cancellationReason")]
		public string CancellationReason { get; set; }

		/// <summary>
		/// Дія учасника.
		/// </summary>
		[JsonProperty("tendererAction")]
		public string TendererAction { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата дії учасника.
		/// </summary>
		[JsonProperty("tendererActionDate")]
		public DateTime? TendererActionDate { get; set; }
	}
}
