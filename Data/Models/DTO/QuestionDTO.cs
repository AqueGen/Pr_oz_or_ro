using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class QuestionDTO : BaseQuestion
    {
        public QuestionDTO()
        {
        }

        public QuestionDTO(IQuestion question)
            : base(question)
        {
        }

        public OrganizationDTO Author { get; set; }
    }
}