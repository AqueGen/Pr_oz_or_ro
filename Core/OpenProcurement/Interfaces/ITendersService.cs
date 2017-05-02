using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Отримання інформації про закупівлі
    /// </summary>
    public interface ITendersService : IGetOrderedModifications, IDocumentsService, IService
    {
        /// <summary>
        /// Зміна критеріїв
        /// </summary>
        /// <param name="tenderId">ідентифікатор закупівлі</param>
        /// <param name="features">нецінові критерії</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        Task<Tender> ChangeFeatures(Guid tenderId, Feature[] features, string acc_token);

        /// <summary>
        /// Cтворення початкового запису закупівлі
        /// </summary>
        /// <param name="tender">при реєстрації закупівлі у базі даних потрібно надати всі основні деталі закупівлі
        /// (окрім двійкових документів) в тілі запиту.</param>
        /// <returns>створений тендер разом с токеном власника</returns>
        Task<Protected<Tender>> CreateTender(Tender tender);

        /// <summary>
        /// Видалення критеріїв
        /// </summary>
        /// <param name="tenderId">ідентифікатор закупівлі</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        Task<Tender> DeleteFeatures(Guid tenderId, string acc_token);

        /// <summary>
        /// Прочитати інформацію про окрему закупівлю
        /// </summary>
        /// <param name="id">ідентифікатор закупівлі</param>
        /// <returns>закупівлю</returns>
        Task<Tender> GetTender(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Зміна початкового запису Закупівлі
        /// </summary>
        /// <param name="id">ідентифікатор закупівлі</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена закупівля</returns>
        Task<Tender> ChangeTender(Guid id, object changes, string acc_token);
    }
}