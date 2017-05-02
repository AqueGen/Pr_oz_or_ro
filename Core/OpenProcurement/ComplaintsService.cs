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
    /// Абстрактний базовий клас для сервісів, які підтримують роботу з Вимогами/Скаргами
    /// </summary>
    public abstract class ComplaintsService : DocumentsService, IComplaintsService
    {
        protected ComplaintsService(string path, CookieContainer cookieContainer = null)
            : base(path, cookieContainer)
        {
        }

        /// <summary>
        /// Надати відповідь.
        /// Замовник закупівлі може відповісти на питання.
        /// </summary>
        /// <param name="id">ідентифікатор питання</param>
        /// <param name="answer">відповідь</param>
        /// <param name="acc_token">токен власника закупівлі</param>
        /// <returns>оновление питання</returns>
        public async Task<Complaint> ChangeComplaint(string id, object changes, string acc_token)
        {
            return await PatchAsync<Complaint>(changes, id, new { acc_token });
        }

        /// <summary>
        /// Подання вимоги/скарги
        /// </summary>
        /// <param name="complaint">вимога/скарга</param>
        /// <returns>вимога/скарга разом с токеном власника</returns>
        public async Task<Protected<Complaint>> CreateComplaint(Complaint complaint)
        {
            return await PostAsync<Protected<Complaint>>(complaint);
        }

        /// <summary>
        /// Перевірити окрему скаргу чи вимогу
        /// </summary>
        /// <param name="id">ідентифікатор вимоги/скарги</param>
        /// <returns>вимога/скарга</returns>
        public async Task<Complaint> GetComplaint(string id)
        {
            return await GetAsync<Complaint>(id);
        }

        /// <summary>
        /// Отримання інформації про вимоги/скарги
        /// </summary>
        /// <returns>список вимог/скарг</returns>
        public async Task<Complaint[]> GetComplaints()
        {
            return await GetAsync<Complaint[]>();
        }
    }
}