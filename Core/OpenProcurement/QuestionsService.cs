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
    /// Питання замовнику
    /// </summary>
    public class QuestionsService : Service, IQuestionsService
    {
        public QuestionsService(Guid tenderId, CookieContainer cookieContainer = null)
            : base("tenders/" + tenderId.ToString("N") + "/questions", cookieContainer)
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
        public async Task<Question> AnswerQuestion(string id, string answer, string acc_token)
        {
            return await PatchAsync<Question>(new { answer }, id, new { acc_token });
        }

        /// <summary>
        /// Питання замовнику.
        /// Будь-хто(але не анонімно) може задати питання.
        /// </summary>
        /// <param name="question">питання</param>
        /// <returns>задане питання разом с токеном власника</returns>
        public async Task<Question> AskQuestion(Question question)
        {
            return await PostAsync<Question>(question);
        }

        /// <summary>
        /// Вичитка питання.
        /// До закінчення аукціону особистість автора питання не розкривається.
        /// </summary>
        /// <param name="id">ідентифікатор питання</param>
        /// <returns>питання</returns>
        public async Task<Question> GetQuestion(string id)
        {
            return await GetAsync<Question>(id);
        }

        /// <summary>
        /// Перегляд питань.
        /// До закінчення аукціону особистість автора питання не розкривається.
        /// </summary>
        /// <returns>список питань</returns>
        public async Task<Question[]> GetQuestions()
        {
            return await GetAsync<Question[]>();
        }
    }
}