using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDraftTenderLimitedViewModel : ICause
    {
        string ProcurementMethodType { get; set; }
        IEnumerable<IDraftDocumentViewModel> Documents { get; set; }
    }
}