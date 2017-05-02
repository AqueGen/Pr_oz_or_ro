using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Data.Store.Models
{
	public class ClassificationScheme
	{
		public int Id { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[StringLength(16)]
		[JsonProperty("code")]
		public string Scheme { get; set; }

		[Required]
		[StringLength(256)]
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[Required]
		[StringLength(2)]
		[JsonProperty("category")]
		public string Category { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("public-database")]
		public bool Public { get; set; }
	}
}
