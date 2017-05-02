using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderBelowThresholdViewModel : BaseDraftTenderViewModel, IDraftTenderBelowThresholdViewModel, IValidatableObject
    {
        public DraftTenderBelowThresholdViewModel()
        {
        }

        public DraftTenderBelowThresholdViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Lots = tender.Lots?.Select(m => new DraftLotViewModel(tender.Guid, m));
                Features = tender.Features?.Select(m => new FeatureViewModel(tender.Guid, m));
                EnquiryPeriod = new PeriodViewModel(tender.EnquiryPeriod);
                TenderPeriod = new PeriodViewModel(tender.TenderPeriod);
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
            }
        }

        public IEnumerable<IDraftLotViewModel> Lots { get; set; }
        public IEnumerable<IFeatureViewModel> Features { get; set; }


        public override string ProcurementMethodType { get; set; } = Data.Models.Consts.ProcurementMethodType.BELOW_THRESHOLD;

        public PeriodViewModel EnquiryPeriod { get; set; }
        public PeriodViewModel TenderPeriod { get; set; }
        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.EnquiryPeriod = new EnquiryPeriod(EnquiryPeriod.ToDTO());
            tender.TenderPeriod = TenderPeriod.ToDTO();
            return tender;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext);
            foreach (var result in errors)
            {
                yield return result;
            }

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