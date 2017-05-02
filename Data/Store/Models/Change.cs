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
	/// Change
	/// </summary>
	public class Change : BaseChange, IChange
	{
		public Change()
		{
		}

		public Change(IChange change)
			: base(change)
		{
		}

		public int Id { get; set; }

		public int RevisionId { get; set; }

		public virtual Revision Revision { get; set; }
	}
}
