using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseDraftLimitedViewModel : IBaseDraftTenderViewModel
    {
        string ProcurementMethodRationale { get; set; }
        IEnumerable<IDraftItemViewModel> Items { get; set; }
    }
}