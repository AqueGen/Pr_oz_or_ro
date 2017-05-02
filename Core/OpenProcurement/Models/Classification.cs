using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Класифікація
	/// </summary>
	public class Classification : BaseClassification, IClassification
	{
		public Classification()
		{
		}

		public Classification(IClassification classification)
			: base (classification)
		{
		}
	}
}
