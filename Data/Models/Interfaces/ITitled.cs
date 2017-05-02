using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface ITitled
	{
		/// <summary>
		/// Назва об'єкта.
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// Детальний опис об'єкта.
		/// </summary>
		string Description { get; set; }
	}
}
