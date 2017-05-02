using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderAboveThresholdEUViewModel : BaseDraftTenderViewModel, IDraftTenderAboveThresholdEUViewModel, IValidatableObject
    {
        public DraftTenderAboveThresholdEUViewModel()
        {
        }

        public DraftTenderAboveThresholdEUViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                TitleEn = tender.TitleEn;
                Lots = tender.Lots?.Select(m => new DraftLotEUViewModel(tender.Guid, m));
                Features = tender.Features?.Select(m => new FeatureEUViewModel(tender.Guid, m));
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
                TenderPeriod = new PeriodViewModel(tender.TenderPeriod);
                EnquiryPeriod = new PeriodViewModel(tender.EnquiryPeriod);
                InvalidationDate = tender.EnquiryPeriod?.InvalidationDate;
                ClarificationsUntil = tender.EnquiryPeriod?.ClarificationsUntil;
                Contacts = tender.Contacts?.Select(m => new ContactViewModel(tender.Guid, m));
            }
        }

        public IEnumerable<ContactViewModel> Contacts { get; set; }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_EU;

        [Required]
        public string TitleEn { get; set; }

        public IEnumerable<IDraftLotEUViewModel> Lots { get; set; }
        public IEnumerable<IFeatureEUViewModel> Features { get; set; }

        public DateTime? InvalidationDate { get; set; }
        public DateTime? ClarificationsUntil { get; set; }

        public PeriodViewModel EnquiryPeriod { get; set; }
        public PeriodViewModel TenderPeriod { get; set; }
        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.TitleEn = TitleEn;
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.TenderPeriod = TenderPeriod.ToDTO();
            tender.EnquiryPeriod = new EnquiryPeriod(EnquiryPeriod.ToDTO());
            if (tender.EnquiryPeriod != null)
            {
                tender.EnquiryPeriod.InvalidationDate = InvalidationDate;
                tender.EnquiryPeriod.ClarificationsUntil = ClarificationsUntil;
            }
            return tender;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EnquiryPeriod.EndDate == null)
            {
                yield return
                    new ValidationResult("Повинна бути вказана хоча б endDate дата.", new[] { "EnquiryPeriod.EndDate" });
            }

            if (TenderPeriod.EndDate == null)
            {
                yield return
                    new ValidationResult("Повинна бути вказана хоча б endDate дата.", new[] { "TenderPeriod.EndDate" });
            }

            if (DateTime.UtcNow.AddDays(30) > TenderPeriod.EndDate)
            {
                yield return
                    new ValidationResult("Тривалість періоду не може бути меншою, ніж 30 календарних днів.", new[] { "TenderPeriod.EndDate" });
            }


            if (EnquiryPeriod.StartDate > EnquiryPeriod.EndDate)
            {
                yield return
                    new ValidationResult("Початок дати не може бути більше кінцевої дати.",
                        new[] { "EnquiryPeriod.StartDate" });
                yield return
                    new ValidationResult("Кінець дати не може бути менше початкової дати.",
                        new[] { "EnquiryPeriod.EndDate" });
            }
            if (TenderPeriod.StartDate > TenderPeriod.EndDate)
            {
                yield return
                    new ValidationResult("Початок дати не може бути більше кінцевої дати.",
                        new[] { "TenderPeriod.StartDate" });
                yield return
                    new ValidationResult("Кінець дати не може бути менше початкової дати.",
                        new[] { "TenderPeriod.EndDate" });
            }

            if ((EnquiryPeriod.EndDate > TenderPeriod.StartDate) || (EnquiryPeriod.EndDate > TenderPeriod.EndDate))
            {
                yield return
                    new ValidationResult("Початок прийому пропозицій повинен бути пізніше ніж дата закінчення питань.",
                        new[] { "TenderPeriod.StartDate" });
                yield return
                    new ValidationResult("Кінець прийому пропозицій повинен бути пізніше ніж дата закінчення питань.",
                        new[] { "TenderPeriod.EndDate" });
            }
        }
    }
}