using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IItemViewModel : IBaseItemViewModel
    {
        IEnumerable<IDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureViewModel> Features { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}