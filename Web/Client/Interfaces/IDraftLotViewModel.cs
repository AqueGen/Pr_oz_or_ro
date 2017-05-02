using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftLotViewModel : IBaseLotViewModel
    {
        IEnumerable<IFeatureViewModel> Features { get; set; }
        IEnumerable<IDraftItemViewModel> Items { get; set; }
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
    }
}