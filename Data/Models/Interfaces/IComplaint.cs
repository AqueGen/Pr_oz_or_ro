using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IComplaint : IStringId, ITitled
	{
		/// <summary>
		/// Генерується автоматично.
		/// Дата подання.
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата, коли вимога була подана.
		/// </summary>
		DateTime? DateSubmitted { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата, коли замовник відповів на вимогу.
		/// </summary>
		DateTime? DateAnswered { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата ескалації (перетворення вимоги на скаргу).
		/// </summary>
		DateTime? DateEscalated { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// День прийняття рішення по вимозі.
		/// </summary>
		DateTime? DateDecision { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата відхилення.
		/// </summary>
		DateTime? DateCanceled { get; set; }

		/// <summary>
		/// Статус скарги
		/// </summary>
		string Status { get; set; }

		/// <summary>
		/// Тип скарги
		/// </summary>
		ComplaintType Type { get; set; }

		/// <summary>
		/// Рішення замовника.
		/// </summary>
		string Resolution { get; set; }

		/// <summary>
		/// Тип вирішення
		/// </summary>
		ResolutionType ResolutionType { get; set; }

		/// <summary>
		/// Вимога задовільнена?
		/// </summary>
		bool Satisfied { get; set; }

		/// <summary>
		/// Рішення органу оскарження.
		/// </summary>
		string Decision { get; set; }

		/// <summary>
		/// Причини відхилення.
		/// </summary>
		string CancellationReason { get; set; }

		/// <summary>
		/// Дія учасника.
		/// </summary>
		string TendererAction { get; set; }

		/// <summary>
		/// Генерується автоматично.
		/// Дата дії учасника.
		/// </summary>
		DateTime? TendererActionDate { get; set; }
	}
}
