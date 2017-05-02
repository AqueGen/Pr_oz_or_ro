using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IRevision
	{
		/// <summary>
		/// Дата, коли зміни були записані.
		/// </summary>
		DateTime Date { get; set; }
	}
}
