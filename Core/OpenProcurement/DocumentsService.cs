using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Core.OpenProcurement
{
    /// <summary>
    /// Абстрактний базовий клас для сервісів, які підтримують роботу з документами
    /// </summary>
    public abstract class DocumentsService : Service, IDocumentsService
    {
        protected DocumentsService(string path, CookieContainer cookieContainer = null, bool readOnly = false)
            : base(path, cookieContainer, readOnly)
        {
        }

        /// <summary>
        /// Прочитати список документів
        /// </summary>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <returns>масив документів</returns>
        public async Task<Document[]> GetDocuments(Guid parentId)
        {
            return await GetAsync<Document[]>(parentId.ToString("N") + "/documents");
        }

        /// <summary>
        /// Прочитати інформацію про документ
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <returns>документ</returns>
        public async Task<Document> GetDocument(Guid id, Guid parentId)
        {
            return await GetAsync<Document>(parentId.ToString("N") + "/documents/" + id.ToString("N"));
        }

        /// <summary>
        /// POST
        /// Получити Uri, на який можна завантажити новий документ
        /// </summary>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>Uri, на який можна завантажити новий документ</returns>
        public Uri GetUploadDocumentUrl(Guid parentId, string acc_token)
        {
            return new Uri(_client.BaseAddress, GetRequestUri(parentId.ToString("N") + "/documents", new { acc_token }));
        }

        /// <summary>
        /// PUT
        /// Получити Uri, на який можна перезавантажити оновлений документ
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>Uri, на який можна перезавантажити оновлений документ для закупівлі</returns>
        public Uri GetUpdateDocumentUrl(Guid id, Guid parentId, string acc_token)
        {
            return new Uri(_client.BaseAddress, GetRequestUri(parentId.ToString("N") + "/documents/" + id.ToString("N"), new { acc_token }));
        }

        /// <summary>
        /// Змінити будь-яке поле в документі
        /// </summary>
        /// <param name="id">ідентифікатор документу</param>
        /// <param name="parentId">ідентифікатор пов'язяного батьківського об'єкта</param>
        /// <param name="changes">наір полів, які потрібно оновити</param>
        /// <param name="acc_token">токен власника</param>
        /// <returns>оновлений документ</returns>
        public async Task<Document> ChangeDocument(Guid id, Guid parentId, object changes, string acc_token)
        {
            return await PatchAsync<Document>(changes, parentId.ToString("N") + "/documents/" + id.ToString("N"), new { acc_token });
        }
    }
}
