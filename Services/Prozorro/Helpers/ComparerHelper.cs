using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Helpers
{
	static class ComparerHelper
	{
		/// <summary>
		/// Розширяючий метод, що дозволяє компаратору порівняти два списки.
		/// </summary>
		/// <typeparam name="TEntity">Тип сутностей БД.</typeparam>
		/// <typeparam name="TUpdate">Тип DTO об'єктів, що містять оновлення.</typeparam>
		/// <typeparam name="TInterface">Інтерфейс, який використовує компаратор для порівняння</typeparam>
		/// <param name="comparrer">Компаратор збережених сутностей з DTO оновлення</param>
		/// <param name="saved">Список збережених сутностей.</param>
		/// <param name="rest">Список DTO з оновленням.</param>
		/// <returns>Зміни{видалити, оновити, додати}</returns>
		public static Changes<TEntity, TUpdate> Compare<TEntity, TUpdate, TInterface>
			(this IEqualityComparer<TInterface> comparrer, IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest)
					where TEntity : class, TInterface
					where TUpdate : class, TInterface
		{
			Dictionary<TInterface, TEntity> old = saved.ToDictionary(i => i, comparrer);
			Dictionary<TEntity, TUpdate> toUpdate = new Dictionary<TEntity, TUpdate>(comparrer);
			HashSet<TUpdate> toAdd = new HashSet<TUpdate>(comparrer);

			foreach (var item in rest)
			{
				TEntity oldItem;
				if (old.TryGetValue(item, out oldItem))
				{
					toUpdate.Add(oldItem, item);
					old.Remove(oldItem);
				}
				else if (!toAdd.Add(item))
					throw new Exception($"The same {typeof(TUpdate).Name} is allready added.");
			}

			return new Changes<TEntity, TUpdate> {
				ToDelete = old.Values.ToList(),
				ToUpdate = toUpdate,
				ToAdd = toAdd
			};
		}

		/// <summary>
		/// Розширяючий метод, що дозволяє компаратору безпечно порівняти два списки, опрацьовуючи null.
		/// </summary>
		/// <typeparam name="TEntity">Тип сутностей БД.</typeparam>
		/// <typeparam name="TUpdate">Тип DTO об'єктів, що містять оновлення.</typeparam>
		/// <typeparam name="TInterface">Інтерфейс, який використовує компаратор для порівняння</typeparam>
		/// <param name="comparrer">Компаратор збережених сутностей з DTO оновлення</param>
		/// <param name="saved">Список збережених сутностей.</param>
		/// <param name="rest">Список DTO з оновленням.</param>
		/// <returns>Зміни{видалити, оновити, додати}</returns>
		public static Changes<TEntity, TUpdate> CompareSafe<TEntity, TUpdate, TInterface>
			(this IEqualityComparer<TInterface> comparrer, IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest)
					where TEntity : class, TInterface
					where TUpdate : class, TInterface
		{
			if (saved == null || rest == null)
			{
				return new Changes<TEntity, TUpdate> {
					ToDelete = saved?.ToList() ?? new List<TEntity>(0),
					ToUpdate = new Dictionary<TEntity, TUpdate>(0),
					ToAdd = rest?.ToList() ?? new List<TUpdate>(0)
				};
			}
			return Compare(comparrer, saved, rest);
		}


		/// <summary>
		/// Порівнює два списка, не порівнюючи відповідні елементи.
		/// Максимальне число елементів старого списку буде оновлено новими елементами.
		/// Інші старі елементи будуть видалені, а інші нові елементи додані.
		/// </summary>
		public static Changes<TEntity, TUpdate> CompareSimple<TEntity, TUpdate>(IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest)
			where TEntity : class
			where TUpdate : class
		{
			var toDelete = new List<TEntity>();
			var toUpdate = new Dictionary<TEntity, TUpdate>();
			var toAdd = new List<TUpdate>();
			using (var s = saved.GetEnumerator())
			{
				using (var r = rest.GetEnumerator())
				{
					while (s.MoveNext())
					{
						if (r.MoveNext())
							toUpdate.Add(s.Current, r.Current);
						else
							toDelete.Add(s.Current);
					}
					while (r.MoveNext())
						toAdd.Add(r.Current);
				}
				return new Changes<TEntity, TUpdate> {
					ToDelete = toDelete,
					ToUpdate = toUpdate,
					ToAdd = toAdd
				};
			}
		}
	}

	public struct Changes<TEntity, TUpdate>
		where TEntity : class
		where TUpdate : class
	{
		public ICollection<TEntity> ToDelete;
		public IDictionary<TEntity, TUpdate> ToUpdate;
		public ICollection<TUpdate> ToAdd;
	}
}
