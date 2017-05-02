using System;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class PlanViewModel
    {
        public Guid Guid { get; set; }
        public DateTime DateModified { get; set; }

        public BudgetViewModel Budget { get; set; }
        public PlanTenderViewModel Tender { get; set; }
        public ProcuringEntityViewModel ProcuringEntity { get; set; }
        public ClassificationViewModel Classification { get; set; }

        public string Identifier { get; set; }
        public string DatePublish { get; set; }
        public string Owner { get; set; }

        public PlanViewModel() : base()
        {
        }

        public PlanViewModel(PlanDTO planDTO)
        {
            if (planDTO != null)
            {
                Guid = planDTO.Guid;
                DateModified = planDTO.DateModified;
                Budget = new BudgetViewModel(planDTO.Budget);
                Tender = new PlanTenderViewModel(planDTO.Tender);
                Classification = new ClassificationViewModel(planDTO.Classification);
            }
        }


        public PlanDTO ToDTO()
        {
            var dto = new PlanDTO
            {
                Budget = Budget.ToDTO(),
                Tender = Tender.ToDTO(),
                Classification = Classification.ToDTO(),
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