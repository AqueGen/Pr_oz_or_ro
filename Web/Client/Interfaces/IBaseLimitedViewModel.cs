using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseLimitedViewModel : IBaseTenderViewModel
    {
        string ProcurementMethodRationale { get; set; }
        IEnumerable<IItemViewModel> Items { get; set; }
    }
}