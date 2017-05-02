using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderLimitedQuickViewModel : BaseLimitedViewModel, ITenderLimitedQuickViewModel
    {
        public TenderLimitedQuickViewModel()
        {
        }

        public TenderLimitedQuickViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Cause = tender.Cause;
                CauseDescription = tender.CauseDescription;
                Documents = tender.Documents?.Select(m => new DocumentViewModel(tender.Guid, m));
            }
        }

        public string Cause { get; set; }
        public string CauseDescription { get; set; }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.NEGOTIATION_QUICK;

        public IEnumerable<IDocumentViewModel> Documents { get; set; }


        public override TenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.Cause = Cause;
            tender.CauseDescription = CauseDescription;
            return tender;
        }
    }
}