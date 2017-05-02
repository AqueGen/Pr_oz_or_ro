using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftItemViewModel : IBaseItemViewModel
    {
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
    }
}