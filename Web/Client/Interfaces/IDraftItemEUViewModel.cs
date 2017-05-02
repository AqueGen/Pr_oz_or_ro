using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftItemEUViewModel : IBaseItemViewModel, IDescriptionEn
    {
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
    }
}