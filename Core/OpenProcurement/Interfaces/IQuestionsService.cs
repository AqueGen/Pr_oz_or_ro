using Kapitalist.Core.OpenProcurement.Models;
using System;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Interfaces
{
    /// <summary>
    /// Питання замовнику
    /// </summary>
    public interface IQuestionsService : IService
    {
        /// <summary>
        /// Надати відповідь.
        /// Замовник закупівлі може відповісти на питання.
        /// </summary>
        /// <param name="id">ідентифікатор питання</param>
        /// <param name="answer">відповідь</param>
        /// <param name="acc_token">токен власника закупівлі</param>
        /// <returns>оновление питання</returns>
        Task<Question> AnswerQuestion(string id, string answer, string acc_token);

        /// <summary>
        /// Питання замовнику.
        /// Будь-хто(але не анонімно) може задати питання.
        /// </summary>
        /// <param name="question">питання</param>
        /// <returns>задане питання разом с токеном власника</returns>
        Task<Question> AskQuestion(Question question);

        /// <summary>
        /// Вичитка питання.
        /// До закінчення аукціону особистість автора питання не розкривається.
        /// </summary>
        /// <param name="id">ідентифікатор питання</param>
        /// <returns>питання</returns>
        Task<Question> GetQuestion(string id);

        /// <summary>
        /// Перегляд питань.
        /// До закінчення аукціону особистість автора питання не розкривається.
        /// </summary>
        /// <returns>список питань</returns>
        Task<Question[]> GetQuestions();
    }
}