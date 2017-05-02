using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderAboveThresholdEUViewModel : IBaseDraftTenderViewModel,
        IEnqueryTenderPeriod, IClarificationUntilInvalidationDate, ITitleEn
    {
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IDraftLotEUViewModel> Lots { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
    }
}