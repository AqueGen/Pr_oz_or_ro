using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class QuestionAuthor : Organization<QuestionAuthorIdentifier>
	{
		public QuestionAuthor()
		{
		}

		public QuestionAuthor(IOrganization organization)
			: base(organization)
		{
		}

		public virtual Question Question { get; set; }
	}

	public class QuestionAuthorIdentifier : Identifier<QuestionAuthor>
	{
		public QuestionAuthorIdentifier()
		{
		}

		public QuestionAuthorIdentifier(IIdentifier identifier)
			: base(identifier)
		{
		}
	}
}
