using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderLimitedQuickViewModel : IBaseTenderViewModel, ICause
    {
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}