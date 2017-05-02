using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderLimitedViewModel : BaseDraftLimitedViewModel, IDraftTenderLimitedViewModel, IValidatableObject
    {
        public DraftTenderLimitedViewModel()
        {
        }

        public DraftTenderLimitedViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Cause = tender.Cause;
                CauseDescription = tender.CauseDescription;
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
            }
        }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.NEGOTIATION;

        [Required]
        public string Cause { get; set; }

        [Required]
        public string CauseDescription { get; set; }

        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.Cause = Cause;
            tender.CauseDescription = CauseDescription;
            return tender;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext).ToList();

            //if (DateTime.UtcNow.AddDays(15) > TenderPeriod.EndDate)
            //{
            //    errors.Add(new ValidationResult("Тривалість періоду не може бути меншою, ніж 15 календарних днів.",
            //        new[] { "TenderPeriod.EndDate" }));
            //}

            return errors;
        }
    }
}