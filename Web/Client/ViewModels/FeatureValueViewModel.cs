using System;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels
{
    public class FeatureValueViewModel : BaseFeatureValueViewModel, IFeatureValueViewModel
    {
        public FeatureValueViewModel()
        {
        }

        public FeatureValueViewModel(Guid tenderGuid, FeatureValueDTO featureValueDTO) : base(tenderGuid, featureValueDTO)
        {
        }


        public override FeatureValueDTO ToDTO()
        {
            var featureValue = base.ToDTO();
            return featureValue;
        }
    }
}