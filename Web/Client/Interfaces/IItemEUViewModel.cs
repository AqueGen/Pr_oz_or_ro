using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IItemEUViewModel : IBaseItemViewModel, IDescriptionEn
    {
        IEnumerable<IDocumentViewModel> Documents { get; set; }
        IEnumerable<IFeatureEUViewModel> Features { get; set; }
        IEnumerable<QuestionViewModel> Questions { get; set; }
    }
}