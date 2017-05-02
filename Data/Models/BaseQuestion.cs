using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Запитання
	/// </summary>
	public abstract class BaseQuestion : IQuestion
	{
		public BaseQuestion()
		{
		}

		public BaseQuestion(IQuestion question)
		{
			StringId = question.StringId;
			Title = question.Title;
			Description = question.Description;
			Date = question.Date;
			Answer = question.Answer;
			QuestionOf = question.QuestionOf;
            RelatedItem = question.RelatedItem;
		}

        /// <summary>
        /// uid, генерується автоматично
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Назва запитання.
		/// </summary>
		[Required (AllowEmptyStrings = true)]
		[JsonRequired]
		[JsonProperty("title")]
		public string Title { get; set; }

		/// <summary>
		/// Опис запитання.
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// Дата публікації.
		/// Генерується автоматично.
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Відповідь на задане питання.
		/// </summary>
		[JsonProperty("answer")]
		public string Answer { get; set; }

		/// <summary>
		/// Тип запитання
		/// </summary>
		[JsonProperty("questionOf")]
		public RelatedTo QuestionOf { get; set; }

		/// <summary>
		/// Guid пов’язаних Lot або Item.
		/// </summary>
		[JsonProperty("relatedItem")]
        [StringLength(64)]
        public string RelatedItem { get; set; }
	}
}
