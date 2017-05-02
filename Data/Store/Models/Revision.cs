using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	/// <summary>
	/// Зміни властивостей об’єктів Закупівлі
	/// </summary>
	public class Revision : BaseRevision, IRevision
	{
		public Revision()
		{
		}

		public Revision(IRevision revision)
			: base(revision)
		{
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }

		/// <summary>
		/// Список об’єктів Change
		/// </summary>
		public virtual ICollection<Change> Changes { get; set; }
	}
}
