using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IQuestion : IStringId, ITitled
	{
		/// <summary>
		/// Дата публікації.
		/// Генерується автоматично.
		/// </summary>
		DateTime Date { get; set; }

		/// <summary>
		/// Відповідь на задане питання.
		/// </summary>
		string Answer { get; set; }

		/// <summary>
		/// Тип запитання
		/// </summary>
		RelatedTo QuestionOf { get; set; }

		/// <summary>
		/// Guid пов’язаних Lot або Item.
		/// </summary>
		string RelatedItem { get; set; }
	}
}
