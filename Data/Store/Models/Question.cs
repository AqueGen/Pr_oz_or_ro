using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Question : BaseQuestion, IQuestion
	{
		public Question()
		{
		}

		public Question(IQuestion question)
			: base(question)
		{
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Хто задає питання (contactPoint - людина, identification - організація, яку ця людина представляє).
		/// </summary>
		public virtual QuestionAuthor Author { get; set; }
	}
}
