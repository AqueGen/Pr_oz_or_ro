using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderAboveThresholdUAViewModel : DraftTenderBelowThresholdViewModel,
        IDraftTenderAboveThresholdUAViewModel, IValidatableObject
    {
        public DraftTenderAboveThresholdUAViewModel()
        {
        }

        public DraftTenderAboveThresholdUAViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                InvalidationDate = tender.EnquiryPeriod?.InvalidationDate;
                ClarificationsUntil = tender.EnquiryPeriod?.ClarificationsUntil;
            }
        }

        public DateTime? InvalidationDate { get; set; }
        public DateTime? ClarificationsUntil { get; set; }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_UA;


        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            if (tender.EnquiryPeriod != null)
            {
                tender.EnquiryPeriod.InvalidationDate = InvalidationDate;
                tender.EnquiryPeriod.ClarificationsUntil = ClarificationsUntil;
            }
            return tender;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext).ToList();

            if (DateTime.UtcNow.AddDays(15) > TenderPeriod.EndDate)
            {
                errors.Add(new ValidationResult("Тривалість періоду не може бути меншою, ніж 15 календарних днів.",
                    new[] {"TenderPeriod.EndDate"}));
            }

            return errors;
        }
    }
}