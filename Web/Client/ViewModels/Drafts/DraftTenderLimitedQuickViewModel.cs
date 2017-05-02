using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderLimitedQuickViewModel : BaseDraftLimitedViewModel, IDraftTenderLimitedQuickViewModel
    {
        public DraftTenderLimitedQuickViewModel()
        {
        }

        public DraftTenderLimitedQuickViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Cause = tender.Cause;
                CauseDescription = tender.CauseDescription;
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
            }
        }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.NEGOTIATION_QUICK;

        public string Cause { get; set; }

        [Required]
        public string CauseDescription { get; set; }

        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.CauseDescription = CauseDescription;
            return tender;
        }
    }
}