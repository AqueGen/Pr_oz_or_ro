using System;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Базовий інтерфейс для сервісів, які підтримують роботу з Вимогами/Скаргами
    /// </summary>
    public interface IComplaintsService : IDocumentsService, IService
    {
		/// <summary>
		/// Надати відповідь.
		/// Замовник закупівлі може відповісти на питання.
		/// </summary>
		/// <param name="id">ідентифікатор питання</param>
		/// <param name="answer">відповідь</param>
		/// <param name="acc_token">токен власника закупівлі</param>
		/// <returns>оновление питання</returns>
		Task<Complaint> ChangeComplaint(string id, object changes, string acc_token);

		/// <summary>
		/// Подання вимоги/скарги
		/// </summary>
		/// <param name="complaint">вимога/скарга</param>
		/// <returns>вимога/скарга разом с токеном власника</returns>
		Task<Protected<Complaint>> CreateComplaint(Complaint complaint);

		/// <summary>
		/// Перевірити окрему скаргу чи вимогу
		/// </summary>
		/// <param name="id">ідентифікатор вимоги/скарги</param>
		/// <returns>вимога/скарга</returns>
		Task<Complaint> GetComplaint(string id);

		/// <summary>
		/// Отримання інформації про вимоги/скарги
		/// </summary>
		/// <returns>список вимог/скарг</returns>
		Task<Complaint[]> GetComplaints();
	}
}