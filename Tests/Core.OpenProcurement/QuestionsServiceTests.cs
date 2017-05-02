using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Exceptions;
using Kapitalist.Core.OpenProcurement.Interfaces;
using Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Tests.Kapitalist.Framework;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    [TestClass()]
    public class QuestionsServiceTests : TendersServiceTestsEx<IQuestionsService>
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            Init<QuestionsService>(TendersServiceTests.ValidTender);
        }

        [TestMethod()]
        public async Task AskQuestionTest()
        {
            var question = await Service.AskQuestion(ValidQuestion);
        }

        [TestMethod]
        public async Task AnswerQuestion_Error_Token()
        {
            var ex = await AssertEx.ThrowsAsync<APIStatusCodeException>(async () =>
            {
                var question = await Service.AskQuestion(ValidQuestion);
                question = await Service.AnswerQuestion(question.StringId, "", "");
            });
            Assert.AreEqual(ex.StatusCode, System.Net.HttpStatusCode.Forbidden);
        }

        [TestMethod()]
        public async Task AnswerQuestion_Success()
        {
            var question = await Service.AskQuestion(ValidQuestion);
            question = await Service.AnswerQuestion(question.StringId, "", Token);
        }

        [TestMethod()]
        public async Task GetQuestionTest()
        {
            var created = await Service.AskQuestion(ValidQuestion);
            var question = await Service.GetQuestion(created.StringId);
            Assert.IsNotNull(question);
        }

        [TestMethod()]
        public async Task GetQuestionsTest()
        {
            var question = await Service.AskQuestion(ValidQuestion);
            question = await Service.AskQuestion(ValidQuestion);
            question = await Service.AnswerQuestion(question.StringId, "", Token);
            question = await Service.AnswerQuestion(question.StringId, "answer", Token);
            var questions = await Service.GetQuestions();
            Assert.IsTrue(questions.Length >= 2);
        }

        private static Question _validQuestion;

        public static Question ValidQuestion
        {
            get {
                if (_validQuestion == null)
                    _validQuestion = new Question
                    {
                        Title = "title",
                        Author = new Organization
                        {
                            Name = "Name",
                            Identifier = new Identifier
                            {
                                Scheme = "UA-EDR",
                                Id = "1"
                            },
                            ContactPoint = new ContactPoint
                            {
                                Name = "Name",
                                Telephone = "0"
                            },
                            Address = new Address
                            {
                                CountryName = "Ukraine"
                            }
                        }
                    };
                return _validQuestion;
            }
        }
    }
}