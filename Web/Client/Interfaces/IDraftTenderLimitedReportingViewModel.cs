using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderLimitedReportingViewModel
    {
        string ProcurementMethodType { get; set; }
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
    }
}