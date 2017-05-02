using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderAboveThresholdEUViewModel : BaseTenderViewModel, ITenderAboveThresholdEUViewModel, IValidatableObject
    {
        public TenderAboveThresholdEUViewModel()
        {
        }

        public TenderAboveThresholdEUViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                TitleEn = tender.TitleEn;
                InvalidationDate = tender.EnquiryPeriod?.InvalidationDate;
                ClarificationsUntil = tender.EnquiryPeriod?.ClarificationsUntil;
                Lots = tender.Lots?.Select(m => new LotEUViewModel(tender.Guid, m));
                Features = tender.Features?.Select(m => new FeatureEUViewModel(tender.Guid, m));
                Questions = tender.Questions?.Select(m => new QuestionViewModel(tender.Guid, m));
                Complaints = tender.Complaints?.Select(m => new TenderComplaintViewModel(tender.Guid, m));
                Documents = tender.Documents?.Select(m => new DocumentViewModel(tender.Guid, m));
                EnquiryPeriod = new PeriodViewModel(tender.EnquiryPeriod);
                TenderPeriod = new PeriodViewModel(tender.TenderPeriod);
            }
        }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_EU;

        [Required]
        public string TitleEn { get; set; }

        public IEnumerable<ILotEUViewModel> Lots { get; set; }
        public IEnumerable<IFeatureEUViewModel> Features { get; set; }

        public IEnumerable<ContactViewModel> Contacts { get; set; }
        public IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }

        public DateTime? InvalidationDate { get; set; }
        public DateTime? ClarificationsUntil { get; set; }

        public PeriodViewModel EnquiryPeriod { get; set; }
        public PeriodViewModel TenderPeriod { get; set; }
        public IEnumerable<IDocumentViewModel> Documents { get; set; }


        public override TenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.TitleEn = TitleEn;
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.TenderPeriod = new Period(TenderPeriod?.ToDTO());
            tender.EnquiryPeriod = new EnquiryPeriod(EnquiryPeriod?.ToDTO())
            {
                InvalidationDate = InvalidationDate,
                ClarificationsUntil = ClarificationsUntil
            };
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