using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Відміни
	/// </summary>
	public class CancellationService : DocumentsService, ICancellationService
	{
		public CancellationService(Guid tenderId)
			: base("tenders/" + tenderId.ToString("N") + "/cancellations")
		{
		}
	}
}
