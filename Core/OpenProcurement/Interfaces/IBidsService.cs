using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Пропозиції
    /// </summary>
    public interface IBidsService : IDocumentsService, IService
    {
        /// <summary>
        /// Оновлення пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена пропозиція</returns>
        Task<Bid> ChangeBid(string id, object changes, string acc_token);

        /// <summary>
        /// Реєстрація цінової пропозиції
        /// </summary>
        /// <param name="bid">пропозиція, яку потрібно зареєструвати</param>
        /// <returns>створена пропозиція разом с токеном власника</returns>
        Task<Protected<Bid>> CreateBid(Bid bid);

        /// <summary>
        /// Відміна пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <param name="acc_token">токен власника</param>
        Task<Bid> DeleteBid(string id, string acc_token);

        /// <summary>
        /// Вичитка пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <returns>пропозицію</returns>
        Task<Bid> GetBid(string id);

        /// <summary>
        /// Перегляд списку пропозицій (даної пропозиції)
        /// </summary>
        /// <returns>список пропозицій</returns>
        Task<Bid[]> GetBids();
    }
}