using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models.Drafts
{
	public abstract class BaseDraftDocument : IDraftDocument
	{
		public BaseDraftDocument()
		{
		}

		public BaseDraftDocument(IDraftDocument document)
		{
			StringId = document.StringId;
			DocumentType = document.DocumentType;
			Title = document.Title;
			Description = document.Description;
			Format = document.Format;
			Language = document.Language;
			DocumentOf = document.DocumentOf;
		}

        /// <summary>
        /// Ідентифікатор документа. 
        /// Генерується автоматично.
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Тип документу
		/// </summary>
		[JsonProperty("documentType")]
		[StringLength(32), Truncate]
		public string DocumentType { get; set; }

		/// <summary>
		/// OpenContracting Description: Назва документа.
		/// </summary>
		[JsonProperty("title")]
		public string Title { get; set; }

		/// <summary>
		/// OpenContracting Description: Короткий опис документа. 
		/// Якщо документ не буде доступний онлайн, 
		/// то поле опису можна використати для вказання способу отримання копії документа.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// OpenContracting Description: Формат документа зі списку кодів IANA Media Types
		/// http://www.iana.org/assignments/media-types/, 
		/// з одним додатковим значенням ‘offline/print’, що буде використовуватись, 
		/// коли запис цього документа використовується для опису офлайнової публікації документа.
		/// </summary>
		[JsonProperty("format")]
		[StringLength(128), Truncate]
		public string Format { get; set; }

		/// <summary>
		/// OpenContracting Description: Вказує мову документа, використовуючи 
		/// або двоцифровий код ISO 639-1
		/// https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes, 
		/// або розширений BCP47 language tags
		/// http://www.w3.org/International/articles/language-tags/.
		/// </summary>
		[JsonProperty("language")]
		[StringLength(32), Truncate]
		public string Language { get; set; }

		/// <summary>
		/// Можливі значення:
		/// tender
		/// item
		/// lot
		/// </summary>
		[JsonProperty("documentOf")]
		public RelatedTo DocumentOf { get; set; }
	}
}
