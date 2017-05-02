using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderLimitedReportingViewModel : IBaseTenderViewModel
    {
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}