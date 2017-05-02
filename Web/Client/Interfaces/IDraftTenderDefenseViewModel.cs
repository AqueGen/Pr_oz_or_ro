using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderDefenseViewModel : IBaseDraftTenderViewModel, ITitleEn
    {
        IEnumerable<ContactViewModel> Contacts { get; set; }
        string ProcurementMethodRationale { get; set; }
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<IDraftLotEUViewModel> Lots { get; set; }
    }
}