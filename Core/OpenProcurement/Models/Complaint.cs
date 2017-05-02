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
	/// Скарга
	/// </summary>
	public class Complaint : BaseComplaint
	{
		/// <summary>
		/// Обов'язково!
		/// Організація, яка подає скаргу (contactPoint - людина, identification - організація, яку ця людина представляє).
		/// </summary>
		// TOTO Taras 5: recheck if this field is required
		// Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
		//[JsonRequired]
		[JsonProperty("author")]
		public Organization Author { get; set; }

		/// <summary>
		/// Супровідна документація скарги.
		/// </summary>
		[JsonProperty("documents")]
		public Document[] Documents { get; set; }

		/// <summary>
		/// StringId пов’язаного Lot.
		/// </summary>
		[JsonProperty("relatedLot")]
		public string RelatedLot { get; set; }
	}
}
