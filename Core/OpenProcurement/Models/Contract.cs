using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Договір
	/// </summary>
	public class Contract : BaseContract
	{
        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Award.id вказує на рішення, згідно якого видається договір.
        /// </summary>
        [NotMapped]
        [JsonRequired]
		[JsonProperty("awardID")]
		public string AwardStringId { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Товари, послуги та інші нематеріальні результати у цій угоді. 
		/// Зверніть увагу: Якщо список співпадає з визначенням переможця award, то його не потрібно повторювати.
		/// </summary>
		[JsonProperty("items")]
		public Item[] Items { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// </summary>
		[JsonProperty("suppliers")]
		public Organization[] Suppliers { get; set; }

		/// <summary>
		/// OpenContracting Description: Усі документи та додатки пов’язані з договором, включно з будь-якими повідомленнями.
		/// </summary>
		[JsonProperty("documents")]
		public Document[] Documents { get; set; }
	}
}
