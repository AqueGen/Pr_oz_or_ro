using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IDocument : IStringId, IDraftDocument
	{
		/// <summary>
		/// Генерується автоматично
		/// OpenContracting Description: Пряме посилання на документ чи додаток.
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата, коли документ був опублікований вперше.
		/// </summary>
		DateTime DatePublished { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата, коли документ був змінений востаннє.
		/// </summary>
		DateTime DateModified { get; set; }

		/// <summary>
		/// Guid пов’язаних Lot або Item.
		/// </summary>
		string RelatedItem { get; set; }
	}
}
