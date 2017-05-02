using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class DraftPlanDTO : BasePlan
    {
        public DraftPlanDTO()
        {
        }

        public DraftPlanDTO(IDraftPlan draftPlan)
            : base(draftPlan)
        {
        }

        public ClassificationDTO Classification { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
        public ICollection<ClassificationDTO> AdditionalClassifications { get; set; }
        public OrganizationDTO ProcuringEntity { get; set; }
        public int ProcuringEntityId { get; set; }
        //public int Id { get; set; }
    }
}