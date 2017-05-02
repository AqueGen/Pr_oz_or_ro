using System.Collections.Generic;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IFeatureEUViewModel : IBaseFeatureViewModel, ITitleEn, IDescriptionEn
    {
        IEnumerable<IFeatureValueEUViewModel> Values { get; set; }
    }
}