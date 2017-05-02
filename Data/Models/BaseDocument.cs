using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	public abstract class BaseDocument : BaseDraftDocument, IDocument
	{
		public BaseDocument()
		{
		}

		public BaseDocument(IDraftDocument document)
			: base(document)
		{
		}

		public BaseDocument(IDocument document)
			: base(document)
		{
			Url = document.Url;
			DatePublished = document.DatePublished;
			DateModified = document.DateModified;
            RelatedItem = document.RelatedItem;
		}

		/// <summary>
		/// Генерується автоматично
		/// OpenContracting Description: Пряме посилання на документ чи додаток.
		/// </summary>
		[JsonProperty("url")]
		public string Url { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата, коли документ був опублікований вперше.
		/// </summary>
		[JsonProperty("datePublished")]
		public DateTime DatePublished { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата, коли документ був змінений востаннє.
		/// </summary>
		[JsonProperty("dateModified")]
		public DateTime DateModified { get; set; }

		/// <summary>
		/// Guid пов’язаних Lot або Item.
		/// </summary>
		[JsonProperty("relatedItem")]
        [StringLength(64)]
		public string RelatedItem { get; set; }
	}
}
