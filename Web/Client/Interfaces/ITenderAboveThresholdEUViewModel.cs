using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderAboveThresholdEUViewModel : IBaseTenderViewModel, IEnqueryTenderPeriod, IClarificationUntilInvalidationDate
    {
        IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        IEnumerable<ContactViewModel> Contacts { get; set; }
        IEnumerable<IDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<ILotEUViewModel> Lots { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
        string TitleEn { get; set; }
    }
}