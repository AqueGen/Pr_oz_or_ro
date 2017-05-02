using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    public interface IPlansService : IGetOrderedModifications, IService
    {
        /// <summary>
        /// Прочитати інформацію про окремий план
        /// </summary>
        /// <param name="id">ідентифікатор плану</param>
        /// <returns>план</returns>
        Task<Plan> GetPlan(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Cтворення початкового запису плану
        /// </summary>
        /// <param name="plan">план</param>
        /// <returns>створений план разом с токеном власника</returns>
        Task<Protected<Plan>> CreatePlan(Plan plan);

        /// <summary>
        /// Зміна початкового запису плану
        /// </summary>
        /// <param name="id">ідентифікатор плану</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений план</returns>
        Task<Plan> ChangePlan(Guid id, object changes, string acc_token);
    }
}