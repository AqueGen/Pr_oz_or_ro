using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Models.Root;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    public interface IGetOrderedModifications : IService
    {
        /// <summary>
        /// Отримати список змінених елементів
        /// Повернені елементи просортовані за датою модифікації.
        /// Якщо запит наступної сторінки повертається без даних (наприклад, пустий масив), тоді немає сенсу викликати сторінки далі.
        /// </summary>
        /// <param name="offset">це параметр, який ви повинні додати до вихідного запиту, щоб отримати наступну сторінку.</param>
        /// <param name="limit">максимальна кількість data записів потоку даних закупівлі (розмір пакета). За промовчанням 100.</param>
        /// <returns>сторінку з масивом ідентифікаторів закупівель і часом їх останньої модифікації та посиланням на наступну сторінку</returns>
        Task<ModificationsPage> GetModificationsPage(DateTime? offset = null, bool reverse = false, int limit = 100, CancellationToken cancellationToken = default(CancellationToken));
    }
}