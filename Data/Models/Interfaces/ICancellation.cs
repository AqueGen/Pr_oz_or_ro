using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface ICancellation : IStringId
    {
		/// <summary>
		/// Причина, з якої скасовується закупівля.
		/// </summary>
		string Reason { get; set; }

		/// <summary>
		/// Статус скасування
		/// </summary>
		string Status { get; set; }

		/// <summary>
		/// Дата скасування
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Тип скасування.
		/// </summary>
		CancellationType CancellationType { get; set; }
	}
}
