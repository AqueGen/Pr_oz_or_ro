using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderDefenseViewModel : BaseDraftTenderViewModel, IDraftTenderDefenseViewModel,
        IValidatableObject
    {
        public DraftTenderDefenseViewModel()
        {
        }

        public DraftTenderDefenseViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                ProcurementMethodRationale = tender.ProcurementMethodRationale;
                TitleEn = tender.TitleEn;
                Features = tender.Features?.Select(m => new FeatureEUViewModel(tender.Guid, m));
                Lots = tender.Lots?.Select(m => new DraftLotEUViewModel(tender.Guid, m));
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
                Contacts = tender.Contacts?.Select(m => new ContactViewModel(tender.Guid, m));
                TenderPeriod = new PeriodViewModel(tender.TenderPeriod);
            }
        }

        public PeriodViewModel TenderPeriod { get; set; }

        public string ProcurementMethodRationale { get; set; }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE;

        public string TitleEn { get; set; }

        public IEnumerable<IFeatureEUViewModel> Features { get; set; }
        public IEnumerable<IDraftLotEUViewModel> Lots { get; set; }

        public IEnumerable<ContactViewModel> Contacts { get; set; }

        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodRationale = ProcurementMethodRationale;
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.TitleEn = TitleEn;
            tender.TenderPeriod = TenderPeriod.ToDTO();
            return tender;
        }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext).ToList();

            if (TenderPeriod.EndDate == null)
            {
                errors.Add(new ValidationResult("Повинна бути вказана хоча б endDate дата.",
                    new[] {"TenderPeriod.EndDate"}));
            }
            if (DateTime.UtcNow.AddDays(6) > TenderPeriod.EndDate)
            {
                errors.Add(new ValidationResult("Тривалість періоду не може бути меншою, ніж 6 календарних днів.",
                    new[] { "TenderPeriod.EndDate" }));
            }


            return errors;
        }
    }
}