using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftTenderLimitedReportingViewModel : BaseDraftLimitedViewModel, IDraftTenderLimitedReportingViewModel
    {
        public DraftTenderLimitedReportingViewModel()
        {
        }

        public DraftTenderLimitedReportingViewModel(DraftTenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Documents = tender.Documents?.Select(m => new DraftDocumentViewModel(tender.Guid, m));
            }
        }

        public override string ProcurementMethodType { get; set; } = Data.Models.Consts.ProcurementMethodType.REPORTING;

        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }


        public override DraftTenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            return tender;
        }
    }
}