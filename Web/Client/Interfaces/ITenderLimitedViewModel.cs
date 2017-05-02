using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface ITenderLimitedViewModel : IBaseTenderViewModel, ICause
    {
        IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}