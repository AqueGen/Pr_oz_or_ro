using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderComplaintViewModel
    {
        public TenderComplaintViewModel()
        {
            SelectListStatus = new List<SelectListItem>
            {
                new SelectListItem {Text = "draft", Value = "draft"},
                new SelectListItem {Text = "claim", Value = "claim"},
                new SelectListItem {Text = "answered", Value = "answered"},
                new SelectListItem {Text = "pending", Value = "pending"},
                new SelectListItem {Text = "invalid", Value = "invalid"},
                new SelectListItem {Text = "declined", Value = "declined"},
                new SelectListItem {Text = "resolved", Value = "resolved"},
                new SelectListItem {Text = "cancelled", Value = "cancelled"},
                new SelectListItem {Text = "accepted", Value = "accepted"}
            };
        }

        public TenderComplaintViewModel(Guid tenderGuid) : this()
        {
            TenderGuid = tenderGuid;
        }

        public TenderComplaintViewModel(Guid tenderGuid, string lotId) : this(tenderGuid)
        {
            LotStringId = lotId;
        }

        public TenderComplaintViewModel(Guid tenderGuid, ComplaintDTO tenderComplaintDTO) : this(tenderGuid)
        {
            if (tenderComplaintDTO != null)
            {
                StringId = tenderComplaintDTO.StringId;
                LotStringId = tenderComplaintDTO.LotStringId;
                Title = tenderComplaintDTO.Title;
                Description = tenderComplaintDTO.Description;
                Date = tenderComplaintDTO.Date;
                DateSubmitted = tenderComplaintDTO.DateSubmitted;
                DateAnswered = tenderComplaintDTO.DateAnswered;
                DateEscalated = tenderComplaintDTO.DateEscalated;
                DateDecision = tenderComplaintDTO.DateDecision;
                DateCanceled = tenderComplaintDTO.DateCanceled;
                Status = tenderComplaintDTO.Status;
                Type = tenderComplaintDTO.Type;
                Resolution = tenderComplaintDTO.Resolution;
                ResolutionType = tenderComplaintDTO.ResolutionType;
                Satisfied = tenderComplaintDTO.Satisfied;
                Decision = tenderComplaintDTO.Decision;
                CancellationReason = tenderComplaintDTO.CancellationReason;
                TendererAction = tenderComplaintDTO.TendererAction;
                TendererActionDate = tenderComplaintDTO.TendererActionDate;
            }
        }

        public string CancellationReason { get; set; }

        public DateTime Date { get; set; }
        public DateTime? DateAnswered { get; set; }
        public DateTime? DateCanceled { get; set; }
        public DateTime? DateDecision { get; set; }
        public DateTime? DateEscalated { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public string Decision { get; set; }

        public string Description { get; set; }
        public string LotStringId { get; set; }
        public string Resolution { get; set; }
        public ResolutionType ResolutionType { get; set; }
        public bool Satisfied { get; set; }

        public IEnumerable<SelectListItem> SelectListStatus { get; set; }
        public string Status { get; set; }
        public string StringId { get; set; }
        public string TendererAction { get; set; }
        public DateTime? TendererActionDate { get; set; }
        public Guid TenderGuid { get; set; }

        [Required]
        public string Title { get; set; }

        public ComplaintType Type { get; set; }

        public TenderComplaintDTO ToDTO()
        {
            var tenderComplaintDTO = new TenderComplaintDTO
            {
                StringId = StringId,
                Description = Description,
                Title = Title,
                LotStringId = LotStringId,
                Status = Status,
                TenderGuid = TenderGuid,
                Type = Type,
                TendererActionDate = TendererActionDate,
                DateCanceled = DateCanceled,
                Satisfied = Satisfied,
                DateDecision = DateDecision,
                DateAnswered = DateAnswered,
                DateEscalated = DateEscalated,
                ResolutionType = ResolutionType,
                Resolution = Resolution,
                Date = Date,
                DateSubmitted = DateSubmitted,
                CancellationReason = CancellationReason,
                Decision = Decision,
                TendererAction = TendererAction
            };
            if (string.IsNullOrWhiteSpace(tenderComplaintDTO.StringId))
            {
                tenderComplaintDTO.StringId = Guid.NewGuid().ToString("N");
            }

            return tenderComplaintDTO;
        }
    }
}