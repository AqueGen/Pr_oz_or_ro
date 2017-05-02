using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderBelowThresholdViewModel : IBaseTenderViewModel, IEnqueryTenderPeriod
    {
        IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
        IEnumerable<ILotViewModel> Lots { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}