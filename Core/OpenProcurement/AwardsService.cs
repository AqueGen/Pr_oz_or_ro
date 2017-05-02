using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Вимоги/Скарги на визначення переможця
    /// </summary>
    public class AwardComplaintsService : ComplaintsService
    {
        public AwardComplaintsService(Guid tenderId, Guid awardId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N") + "/awards/" + awardId.ToString("N") + "/complaints", cookieContainer)
        {
        }
    }

    /// <summary>
    /// Операції кваліфікації
    /// </summary>
    public class AwardsService : DocumentsService, IAwardsService
    {
        public AwardsService(Guid tenderId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N") + "/awards", cookieContainer)
        {
        }

        /// <summary>
        /// Оновлення результату оцінки
        /// </summary>
        /// <param name="id">ідентифікатор винагороди</param>
        /// <param name="changes">набір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлена винагорода</returns>
        public async Task<Award> ChangeAward(Guid id, object changes, string acc_token)
        {
            return await PatchAsync<Award>(changes, id.ToString("N"), new { acc_token });
        }

        /// <summary>
        /// Перегляд результату оцінки
        /// </summary>
        /// <param name="id">ідентифікатор результату оцінки</param>
        /// <returns>винагорода</returns>
        public async Task<Award> GetAward(Guid id)
        {
            return await GetAsync<Award>(id.ToString("N"));
        }

        /// <summary>
        /// Перегляд результатів оцінки
        /// </summary>
        /// <returns>список усіх доступних винагород</returns>
        public async Task<Award[]> GetAwards()
        {
            return await GetAsync<Award[]>();
        }
    }
}