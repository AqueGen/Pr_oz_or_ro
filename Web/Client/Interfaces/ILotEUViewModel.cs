using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ILotEUViewModel : IBaseLotViewModel
    {
        PeriodViewModel AuctionPeriod { get; set; }
        string AuctionUrl { get; set; }
        IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<IItemEUViewModel> Items { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
        string Status { get; set; }
        string TitleEn { get; set; }
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}