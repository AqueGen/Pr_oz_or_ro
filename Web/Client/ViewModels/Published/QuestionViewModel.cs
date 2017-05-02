using System;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class QuestionViewModel
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public string StringId { get; set; }
        public Guid TenderGuid { get; set; }
        public string RelatedGuid { get; set; }
        public DateTime Date { get; set; }
        public string Answer { get; set; }
        public RelatedTo QuestionOf { get; set; }

        public QuestionViewModel()
        {
        }
        public QuestionViewModel(Guid tenderGuid)
        {
            TenderGuid = tenderGuid;
        }

        public QuestionViewModel(Guid tenderGuid, QuestionDTO question): this(tenderGuid)
        {
            if (question != null)
            {
                StringId = question.StringId;
                Answer = question.Answer;
                Date = question.Date;
                Description = question.Description;
                QuestionOf = question.QuestionOf;

                if (string.IsNullOrWhiteSpace(question.RelatedItem))
                {
                    RelatedGuid = tenderGuid.ToString("N");
                }
                else
                {
                    RelatedGuid = question.RelatedItem;
                }
                Title = question.Title;
            }
        }



        public QuestionDTO ToDTO()
        {
            var question =  new QuestionDTO
            {
                StringId = StringId,
                Answer = Answer,
                Date = Date,
                Description = Description,
                QuestionOf = QuestionOf,
                RelatedItem = RelatedGuid,
                Title = Title
            };
            if (string.IsNullOrWhiteSpace(question.StringId))
            {
                question.StringId = Guid.NewGuid().ToString("N");
            }
            return question;
        }
    }
}