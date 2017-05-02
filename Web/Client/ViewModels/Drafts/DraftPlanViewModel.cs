using System;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftPlanViewModel
    {
        public Guid Guid { get; set; }
        public DateTime DateModified { get; set; }

        public BudgetViewModel Budget { get; set; }
        public PlanTenderViewModel Tender { get; set; }
        public ProcuringEntityViewModel ProcuringEntity { get; set; }
        public ClassificationViewModel Classification { get; set; }

        public DraftPlanViewModel()
        {
        }

        public DraftPlanViewModel(DraftPlanDTO draftPlanDTO)
        {
            if(draftPlanDTO != null)
            {
                Guid = draftPlanDTO.Guid;
                DateModified = draftPlanDTO.DateModified;
                Budget = new BudgetViewModel(draftPlanDTO.Budget);
                Tender = new PlanTenderViewModel(draftPlanDTO.Tender);
                Classification = new ClassificationViewModel(draftPlanDTO.Classification);
            }
        }


        public DraftPlanDTO ToDTO()
        {
            var dto = new DraftPlanDTO
            {
                Budget = Budget?.ToDTO(),
                Tender = Tender?.ToDTO(),
                Classification = Classification?.ToDTO(),
                DateModified = DateModified,
                Guid = Guid
            };
            if (Guid == Guid.Empty)
            {
                dto.Guid = Guid.NewGuid();
            }
            return dto;
        }
    }
}