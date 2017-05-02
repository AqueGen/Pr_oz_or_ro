using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Базовий інтерфейс для сервісів, які підтримують роботу з документами
    /// </summary>
    public interface IDocumentsService : IService
    {
        /// <summary>
        /// Змінити будь-яке поле в документі
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений документ</returns>
        Task<Document> ChangeDocument(Guid id, Guid parentId, object changes, string acc_token);

        /// <summary>
        /// Прочитати інформацію про документ
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <returns>документ</returns>
        Task<Document> GetDocument(Guid id, Guid parentId);

        /// <summary>
        /// Прочитати список документів
        /// </summary>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <returns>масив документів</returns>
        Task<Document[]> GetDocuments(Guid parentId);

        /// <summary>
        /// PUT
        /// Получити Uri, на який можна перезавантажити оновлений документ
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>Uri, на який можна перезавантажити оновлений документ для закупівлі</returns>
        Uri GetUpdateDocumentUrl(Guid id, Guid parentId, string acc_token);

        /// <summary>
        /// POST
        /// Получити Uri, на який можна завантажити новий документ
        /// </summary>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>Uri, на який можна завантажити новий документ</returns>
        Uri GetUploadDocumentUrl(Guid parentId, string acc_token);
    }
}