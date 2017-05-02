using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Операції кваліфікації
    /// </summary>
    public interface IAwardsService : IDocumentsService, IService
    {
        /// <summary>
        /// Оновлення результату оцінки
        /// </summary>
        /// <param name="id">ідентифікатор винагороди</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена винагорода</returns>
        Task<Award> ChangeAward(Guid id, object changes, string acc_token);

        /// <summary>
        /// Перегляд результату оцінки
        /// </summary>
        /// <param name="id">ідентифікатор результату оцінки</param>
        /// <returns>винагорода</returns>
        Task<Award> GetAward(Guid id);

        /// <summary>
        /// Перегляд результатів оцінки
        /// </summary>
        /// <returns>список усіх доступних винагород</returns>
        Task<Award[]> GetAwards();
    }
}