using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IParameter
	{
		/// <summary>
		/// Обов'язково!
		/// Код критерію.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Значення критерію.
		/// </summary>
		float Value { get; set; }
	}
}
