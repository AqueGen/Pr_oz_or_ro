using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftLotEUViewModel : IBaseLotViewModel, ITitleEn
    {
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<IDraftItemEUViewModel> Items { get; set; }
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
    }
}