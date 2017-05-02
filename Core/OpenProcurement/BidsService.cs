using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Пропозиції
    /// </summary>
    public class BidsService : DocumentsService, IBidsService
    {
        public BidsService(Guid tenderId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N") + "/bids", cookieContainer)
        {
        }

        /// <summary>
        /// Оновлення пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена пропозиція</returns>
        public async Task<Bid> ChangeBid(string id, object changes, string acc_token)
        {
            return await PatchAsync<Bid>(changes, id, new { acc_token });
        }

        /// <summary>
        /// Реєстрація цінової пропозиції
        /// </summary>
        /// <param name="bid">пропозиція, яку потрібно зареєструвати</param>
        /// <returns>створена пропозиція разом с токеном власника</returns>
        public async Task<Protected<Bid>> CreateBid(Bid bid)
        {
            return await PostAsync<Protected<Bid>>(bid);
        }

        /// <summary>
        /// Відміна пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <param name="acc_token">токен власника</param>
        public async Task<Bid> DeleteBid(string id, string acc_token)
        {
            return await DeleteAsync<Bid>(id, new { acc_token });
        }

        /// <summary>
        /// Вичитка пропозиції
        /// </summary>
        /// <param name="id">ідентифікатор пропрозиції</param>
        /// <returns>пропозицію</returns>
        public async Task<Bid> GetBid(string id)
        {
            return await GetAsync<Bid>(id);
        }

        /// <summary>
        /// Перегляд списку пропозицій (даної пропозиції)
        /// </summary>
        /// <returns>список пропозицій</returns>
        public async Task<Bid[]> GetBids()
        {
            return await GetAsync<Bid[]>();
        }
    }
}