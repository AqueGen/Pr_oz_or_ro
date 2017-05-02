using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IFeatureViewModel : IBaseFeatureViewModel
    {
        IEnumerable<IBaseFeatureValueViewModel> Values { get; set; }
    }
}