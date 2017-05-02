using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Services.Prozorro.Helpers.Comparers
{
	public class DocumentComparer : IEqualityComparer<IDocument>
	{
		public bool Equals(IDocument x, IDocument y)
		{
			return x.StringId.Equals(y.StringId) && x.DateModified.Equals(y.DateModified);
		}

		public int GetHashCode(IDocument obj)
		{
			return obj.StringId.GetHashCode() ^ obj.DateModified.GetHashCode();
		}

		public static DocumentComparer Instance { get; } = new DocumentComparer();
	}
}
