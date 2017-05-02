using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Пропозиція
	/// </summary>
	public class Bid : BaseBid
	{
		/// <summary>
		/// Список організацій
		/// </summary>
		[JsonProperty("tenderers")]
		public Organization[] Tenderers { get; set; }

		/// <summary>
		/// Документи
		/// </summary>
		[JsonProperty("documents")]
		public Document[] Documents { get; set; }

		/// <summary>
		/// Критерії ставки
		/// </summary>
		[JsonProperty("parameters")]
		public Parameter[] Parameters { get; set; }

		/// <summary>
		/// LotValues
		/// </summary>
		[JsonProperty("lotValues")]
		public LotValue[] LotValues { get; set; }
	}
}
