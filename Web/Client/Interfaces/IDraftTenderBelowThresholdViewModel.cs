using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderBelowThresholdViewModel : IBaseDraftTenderViewModel,
        IEnqueryTenderPeriod
    {
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IDraftLotViewModel> Lots { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
    }
}