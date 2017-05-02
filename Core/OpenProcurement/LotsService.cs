using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Лоти закупівлі
    /// </summary>
    public class LotsService : Service, ILotsService
    {
        public LotsService(Guid tenderId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N"), cookieContainer)
        {
        }

        /// <summary>
        /// Елементи повинні бути розподілені по лотах.
        /// </summary>
        /// <param name="lots">словник елементів розподілених по лотах</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена пропозиція</returns>
        public async Task<Tender> AssignLots(IDictionary<Item, Lot> lots, string acc_token)
        {
            var items = lots.Select(l => new { id = l.Key.StringId, relatedLot = l.Value.StringId }).ToArray();
            return await PatchAsync<Tender>(new { items }, null, new { acc_token });
        }

        /// <summary>
        /// Оновлення лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений лот</returns>
        public async Task<Lot> ChangeLot(string id, object changes, string acc_token)
        {
            return await PatchAsync<Lot>(changes, "lots/" + id, new { acc_token });
        }

        /// <summary>
        /// Додати лот
        /// </summary>
        /// <param name="bid">новий лот</param>
        /// <returns>створений лот разом с токеном власника</returns>
        public async Task<Lot> CreateLot(Lot lot, string acc_token)
        {
            return await PostAsync<Lot>(lot, "lots", new { acc_token });
        }

        /// <summary>
        /// Видалення лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <param name="acc_token">токен власника</param>
        public async Task<Lot> DeleteLot(string id, string acc_token)
        {
            return await DeleteAsync<Lot>("lots/" + id, new { acc_token });
        }

        /// <summary>
        /// Вичитка лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <returns>лот</returns>
        public async Task<Lot> GetLot(string id)
        {
            return await GetAsync<Lot>("lots/" + id);
        }

        /// <summary>
        /// Перегляд списку лотів (даної пропозиції)
        /// </summary>
        /// <returns>список лотів</returns>
        public async Task<Lot[]> GetLots()
        {
            return await GetAsync<Lot[]>("lots");
        }
    }
}