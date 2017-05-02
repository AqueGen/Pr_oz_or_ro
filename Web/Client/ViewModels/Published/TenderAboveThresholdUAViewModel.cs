using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderAboveThresholdUAViewModel : TenderBelowThresholdViewModel, ITenderAboveThresholdUAViewModel, IValidatableObject
    {
        public TenderAboveThresholdUAViewModel()
        {
        }

        public TenderAboveThresholdUAViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                InvalidationDate = tender.EnquiryPeriod?.InvalidationDate;
                ClarificationsUntil = tender.EnquiryPeriod?.ClarificationsUntil;
            }
        }

        public DateTime? InvalidationDate { get; set; }
        public DateTime? ClarificationsUntil { get; set; }

        public string ProcurementMethodType = Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_UA;

        public override TenderDTO ToDTO()
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}