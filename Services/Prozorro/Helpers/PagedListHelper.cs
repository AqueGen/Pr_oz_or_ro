using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PagedList;

namespace Kapitalist.Services.Prozorro.Helpers
{
	static class PagedListHelper
	{
		public static async Task<IPagedList<T>> ToPagedListAsync<T, U>(this IQueryable<U> superset, int pageNumber, int pageSize, Func<U, T> mapItem)
		{
			var total = superset == null ? 0 : await superset.CountAsync();
			var mappedPagedList = new MappedPagedList<T, U>(superset, pageNumber, pageSize, total, mapItem);
			await mappedPagedList.InitSubset();
			return mappedPagedList;
		}

		class MappedPagedList<T, U> : BasePagedList<T>
		{
			IQueryable<U> _superset;
			Func<U, T> _mapItem;

			public MappedPagedList(IQueryable<U> superset, int pageNumber, int pageSize, int totalItemCount, Func<U, T> mapItem)
				: base(pageNumber, pageSize, totalItemCount)
			{
				_superset = superset;
				_mapItem = mapItem;
			}

			public async Task InitSubset()
			{
				var skip = PageNumber * PageSize - PageSize;
				List<U> subset = await _superset.Skip(skip).Take(PageSize).ToListAsync();
				Subset.Clear();
				Subset.AddRange(subset.Select(i => _mapItem(i)));
			}
		}
	}
}
