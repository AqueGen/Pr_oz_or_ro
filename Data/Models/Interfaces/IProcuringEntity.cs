using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IProcuringEntity : IOrganization
	{
		/// <summary>
		/// Тип замовника
		/// </summary>
		ProcuringEntityType? Kind { get; set; }
	}
}
