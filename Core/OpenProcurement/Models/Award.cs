using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Рішення
	/// </summary>
	public class Award : BaseAward, IAward
	{
		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// Guid пропозиції, що виграла закупівлю
		/// </summary>
        [NotMapped]
		[JsonProperty("bid_id")]
		public string BidStringId { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Постачальники, що були визнані переможцями згідно цього рішення.
		/// </summary>
		[JsonProperty("suppliers")]
		public Organization[] Suppliers { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Товари та послуги, що розглядались цим рішенням, 
		/// поділені на окремі рядки, де це можливо. 
		/// Елементи не повинні бути продубльовані, а повинні мати вказану кількість.
		/// </summary>
		[JsonProperty("items")]
		public Item[] Items { get; set; }

		/// <summary>
		/// OpenContracting Description: Усі документи та додатки пов’язані з рішенням, включно з будь-якими повідомленнями.
		/// </summary>
		[JsonProperty("documents")]
		public Document[] Documents { get; set; }

		/// <summary>
		/// Список скарг.
		/// </summary>
		[JsonProperty("complaints")]
		public Complaint[] Complaints { get; set; }

		/// <summary>
		/// StringId пов’язаного Lot.
		/// </summary>
		[JsonProperty("lotID")]
		public string LotStringId { get; set; }
	}
}
