using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Services.Prozorro.Helpers.Comparers;
using Kapitalist.Services.Prozorro.Mappers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Kapitalist.Services.Prozorro.Helpers
{
    /// <summary>
    ///
    /// </summary>
    internal static class UpdateHelper
    {
        public static void ReplaceSingle<TEntity, TUpdate>(this DbContext db,
            TEntity saved, TUpdate rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class
            where TUpdate : class
        {
            Replace(db, saved == null ? null : new[] { saved },
                rest == null ? null : new[] { rest }, additionalAction);
        }

        public static void UpdateSingle<TEntity, TUpdate>(this DbContext db,
            TEntity saved, TUpdate rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class, IGuid
            where TUpdate : class, IGuid
        {
            var savedArray = saved == null ? null : new[] { saved };
            var restArray = rest == null ? null : new[] { rest };

            Update(db, savedArray, restArray, () => ComparerHelper.CompareSimple(savedArray, restArray), additionalAction);
        }

        public static void Replace<TEntity, TUpdate>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class
            where TUpdate : class
        {
            Update(db, saved, rest, () => ComparerHelper.CompareSimple(saved, rest), additionalAction);
        }

        public static void UpdateByGuid<TEntity, TUpdate>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class, IGuid
            where TUpdate : class, IGuid
        {
            Update(db, saved, rest, () => GuidComparer.Instance.Compare(saved, rest), additionalAction);
        }

        public static void Update<TEntity, TUpdate>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class, IStringId
            where TUpdate : class, IStringId
        {
            Update(db, saved, rest, () => StringIdComparer.Instance.Compare(saved, rest), additionalAction);
        }

        public static void UpdateDocuments<TEntity, TUpdate>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class, IDocument
            where TUpdate : class, IDocument
        {
            Update(db, saved, rest, () => DocumentComparer.Instance.Compare(saved, rest), additionalAction);
        }

        public static void Update<TEntity, TUpdate, TInterface>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest,
            IEqualityComparer<TInterface> comparer, Action<TEntity, TUpdate> additionalAction = null)
            where TEntity : class, TInterface
            where TUpdate : class, TInterface
        {
            Update(db, saved, rest, () => comparer.Compare(saved, rest), additionalAction);
        }

        /// <summary>
        /// Оновити/замінити список існуючих об'єктів, на новий список, що представлений в DTO.
        /// Використовуючи функцію-детектор змін.
        /// - всі збережені об'єкти, що не мають відповідного DTO - видаляються (каскадно видаляются всі звязані сутності);
        /// - ті, що мають - оновлюються властивостями DTO (не міняючи ключі та звязки);
        /// - для всіх DTO, що не мали відповідної сутності в БД - створюється нова
        /// Додатково, можна задати дію, що повинна виконуватись після оновлення чи додавання сутності.
        /// </summary>
        /// <typeparam name="TEntity">Тип сутностей БД.</typeparam>
        /// <typeparam name="TUpdate">Тип DTO об'єктів, що містять оновлення.</typeparam>
        /// <param name="db">Контекст бази даних.</param>
        /// <param name="saved">Список збережених сутностей.</param>
        /// <param name="rest">Список DTO з оновленням.</param>
        /// <param name="changesDetection">Функція, що порівнює списки і повертає зміни.</param>
        /// <param name="additionalAction">Дія, яку потрібно виконати з кожною парою
        /// збереженого/доданого об'єкта та відповідного DTO,
        /// після проведення базової процедури оновлення.</param>
        private static void Update<TEntity, TUpdate>(this DbContext db,
            IEnumerable<TEntity> saved, IEnumerable<TUpdate> rest,
            Func<Changes<TEntity, TUpdate>> changesDetection,
            Action<TEntity, TUpdate> additionalAction)
            where TEntity : class
            where TUpdate : class
        {
            DbSet<TEntity> set = db.Set<TEntity>();
            Changes<TEntity, TUpdate> changes;

            if (saved == null || rest == null || changesDetection == null)
            {
                changes = new Changes<TEntity, TUpdate>
                {
                    ToDelete = saved?.ToList() ?? new List<TEntity>(0),
                    ToUpdate = new Dictionary<TEntity, TUpdate>(0),
                    ToAdd = rest?.ToList() ?? new List<TUpdate>(0)
                };
            }
            else
            {
                changes = changesDetection();
            }
            // remove
            if (changes.ToDelete.Count > 0)
            {
                set.RemoveRange(changes.ToDelete);
            }
            // update
            foreach (var pair in changes.ToUpdate)
            {
                db.Entry(pair.Key).CurrentValues.SetValues(pair.Value.InitComplexProperties());
                additionalAction?.Invoke(pair.Key, pair.Value);
            }
            // add
            foreach (var item in changes.ToAdd)
            {
                var newItem = (TEntity)Activator.CreateInstance(typeof(TEntity), item);
                set.Add(newItem.InitComplexProperties());
                additionalAction?.Invoke(newItem, item);
            }
        }

        /// <summary>
        /// Оновлює колекцію linked.
        /// Видаляючи з неї ті елементи, які не мають співпадінь в фільтрі.
        /// Та добавляючи до неї елементи списку all, які мають співпадіння в фільтрі.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TUpdate"></typeparam>
        /// <param name="all">Повний список доступних елементів</param>
        /// <param name="linked">Колекція приєднаних елементів, яку потрібно обновити у відповідності з фільтром</param>
        /// <param name="filter">Фільтр елементів, які повинні бути приєднані</param>
        public static ICollection<TEntity> UpdateLinks<TEntity, TUpdate>(this ICollection<TEntity> all,
            ICollection<TEntity> linked, IEnumerable<TUpdate> filter)
            where TEntity : class, IStringId
            where TUpdate : class, IStringId
        {
            var changes = StringIdComparer.Instance.CompareSafe(linked, filter);
            foreach (var item in changes.ToDelete)
                linked.Remove(item);
            changes = StringIdComparer.Instance.CompareSafe(all, changes.ToAdd);
            if (linked == null && changes.ToUpdate.Count > 0)
                linked = new List<TEntity>(changes.ToUpdate.Count);
            foreach (var item in changes.ToUpdate.Keys)
                linked.Add(item);
            // Якщо в фільтрі були елементи, що не знайдені в all - викидуємо вийняток
            if (changes.ToAdd.Count > 0)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with id {changes.ToAdd.First().StringId} cannot be found.");
            return linked;
        }
    }
}