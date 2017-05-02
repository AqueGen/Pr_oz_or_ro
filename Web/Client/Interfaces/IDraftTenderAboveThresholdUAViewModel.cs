using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderAboveThresholdUAViewModel : IBaseDraftTenderViewModel, IEnqueryTenderPeriod,
        IClarificationUntilInvalidationDate
    {
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IDraftLotViewModel> Lots { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
    }
}