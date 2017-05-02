using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
	public interface IDraftDocument : IStringId, ITitled
	{
		/// <summary>
		/// Тип документу
		/// </summary>
		string DocumentType { get; set; }

		/// <summary>
		/// OpenContracting Description: Формат документа зі списку кодів IANA Media Types
		/// http://www.iana.org/assignments/media-types/, 
		/// з одним додатковим значенням ‘offline/print’, що буде використовуватись, 
		/// коли запис цього документа використовується для опису офлайнової публікації документа.
		/// </summary>
		string Format { get; set; }

		/// <summary>
		/// OpenContracting Description: Вказує мову документа, використовуючи 
		/// або двоцифровий код ISO 639-1
		/// https://en.wikipedia.org/wiki/List_of_ISO_639-1_codes, 
		/// або розширений BCP47 language tags
		/// http://www.w3.org/International/articles/language-tags/.
		/// </summary>
		string Language { get; set; }

		/// <summary>
		/// Можливі значення:
		/// tender
		/// item
		/// lot
		/// </summary>
		RelatedTo DocumentOf { get; set; }
	}
}
