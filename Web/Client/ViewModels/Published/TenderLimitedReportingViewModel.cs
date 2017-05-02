using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderLimitedReportingViewModel : BaseLimitedViewModel, ITenderLimitedReportingViewModel
    {
        public TenderLimitedReportingViewModel()
        {
        }

        public TenderLimitedReportingViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Documents = tender.Documents?.Select(m => new DocumentViewModel(tender.Guid, m));
            }
        }

        public override string ProcurementMethodType { get; set; } = Data.Models.Consts.ProcurementMethodType.REPORTING;

        public IEnumerable<IDocumentViewModel> Documents { get; set; }

        public override TenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            return tender;
        }
    }
}