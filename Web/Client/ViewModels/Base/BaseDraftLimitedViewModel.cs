using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Drafts;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public abstract class BaseDraftLimitedViewModel : BaseDraftTenderViewModel, IBaseDraftLimitedViewModel, IValidatableObject
    {
        public BaseDraftLimitedViewModel()
        {
        }

        public BaseDraftLimitedViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Items = tender.Items?.Select(m => new DraftItemViewModel(tender.Guid, m));
                ProcurementMethodRationale = tender.ProcurementMethodRationale;
            }
        }

        public string ProcurementMethodRationale { get; set; }

        public IEnumerable<IDraftItemViewModel> Items { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodRationale = ProcurementMethodRationale;
            return tender;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}