using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement
{
    public class PlansService : DocumentsService, IPlansService
    {
        public PlansService(bool readOnly)
            : base("plans", null, readOnly)
        {
        }

        public PlansService(CookieContainer cookieContainer = null)
            : base("plans", cookieContainer)
        {
        }

        /// <summary>
        /// Отримати список планів
        /// Повернені плани просортовані за датою модифікації.
        /// Якщо запит наступної сторінки повертається без даних (наприклад, пустий масив), тоді немає сенсу викликати сторінки далі.
        /// </summary>
        /// <param name="offset">це параметр, який ви повинні додати до вихідного запиту, щоб отримати наступну сторінку.</param>
        /// <param name="limit">максимальна кількість data записів потоку даних плану (розмір пакета). За промовчанням 100.</param>
        /// <returns>сторінку з масивом ідентифікаторів планів і часом їх останньої модифікації та посиланням на наступну сторінку</returns>
        public async Task<ModificationsPage> GetModificationsPage(DateTime? offset = null, bool reverse = false, int limit = 100, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<ModificationsPage>(new { offset, descending = reverse ? 1 : (int?)null, limit }, cancellationToken);
        }

        /// <summary>
        /// Прочитати інформацію про окремий план
        /// </summary>
        /// <param name="id">ідентифікатор плану</param>
        /// <returns>план</returns>
        public async Task<Plan> GetPlan(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetAsync<Plan>(id.ToString("N"), null, cancellationToken);
        }

        /// <summary>
        /// Cтворення початкового запису плану
        /// </summary>
        /// <param name="plan">план</param>
        /// <returns>створений план разом с токеном власника</returns>
        public async Task<Protected<Plan>> CreatePlan(Plan plan)
        {
            return await PostAsync<Protected<Plan>>(plan);
        }

        /// <summary>
        /// Зміна початкового запису плану
        /// </summary>
        /// <param name="id">ідентифікатор плану</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений план</returns>
        public async Task<Plan> ChangePlan(Guid id, object changes, string acc_token)
        {
            return await PatchAsync<Plan>(changes, id.ToString("N"), new { acc_token });
        }
    }
}