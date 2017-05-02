using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IValue : IGuarantee
	{
		/// <summary>
		/// Обов'язково!
		/// Включено податок на додану вартість?
		/// </summary>
		bool VATIncluded { get; set; }
	}
}
