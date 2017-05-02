using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ILotViewModel : IBaseLotViewModel
    {
        PeriodViewModel AuctionPeriod { get; set; }
        string AuctionUrl { get; set; }
        IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
        IEnumerable<IItemViewModel> Items { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
        string Status { get; set; }
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}