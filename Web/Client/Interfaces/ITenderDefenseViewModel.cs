using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderDefenseViewModel : IBaseTenderViewModel, ITitleEn
    {
        IEnumerable<TenderComplaintViewModel> Complaints { get; set; }
        IEnumerable<ContactViewModel> Contacts { get; set; }
        IEnumerable<IDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<ILotEUViewModel> Lots { get; set; }
        string ProcurementMethodRationale { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}