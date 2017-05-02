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
	/// Об’єкт Cancellation описує причину скасування закупівлі та надає відповідні документи, якщо такі є.
	/// </summary>
	public class Cancellation : BaseCancellation
	{
		/// <summary>
		/// Супровідна документація скасування: Протокол рішення Тендерного комітету Замовника про скасування закупівлі.
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
