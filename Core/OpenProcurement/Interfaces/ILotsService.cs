using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Лоти закупівлі
    /// </summary>
    public interface ILotsService : IService
    {
        /// <summary>
        /// Елементи повинні бути розподілені по лотах.
        /// </summary>
        /// <param name="lots">ловник елементів розподілених по лотах</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена пропозиція</returns>
        Task<Tender> AssignLots(IDictionary<Item, Lot> lots, string acc_token);

        /// <summary>
        /// Оновлення лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений лот</returns>
        Task<Lot> ChangeLot(string id, object changes, string acc_token);

        /// <summary>
        /// Додати лот
        /// </summary>
        /// <param name="bid">новий лот</param>
        /// <returns>створений лот разом с токеном власника</returns>
        Task<Lot> CreateLot(Lot lot, string acc_token);

        /// <summary>
        /// Видалення лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <param name="acc_token">токен власника</param>
        Task<Lot> DeleteLot(string id, string acc_token);

        /// <summary>
        /// Вичитка лота
        /// </summary>
        /// <param name="id">ідентифікатор лота</param>
        /// <returns>лот</returns>
        Task<Lot> GetLot(string id);

        /// <summary>
        /// Перегляд списку лотів (даної пропозиції)
        /// </summary>
        /// <returns>список лотів</returns>
        Task<Lot[]> GetLots();
    }
}