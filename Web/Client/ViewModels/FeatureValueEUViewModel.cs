using System;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels
{
    public class FeatureValueEUViewModel : FeatureValueViewModel, IFeatureValueEUViewModel
    {
        public FeatureValueEUViewModel()
        {
        }

        public FeatureValueEUViewModel(Guid tenderGuid, FeatureValueDTO featureValueDTO) : base(tenderGuid, featureValueDTO)
        {
            if (featureValueDTO != null)
            {
                TitleEn = featureValueDTO.TitleEn;
                DescriptionEn = featureValueDTO.DescriptionEn;
            }
        }

        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }

        public override FeatureValueDTO ToDTO()
        {
            var featureValue = base.ToDTO();
            featureValue.TitleEn = TitleEn;
            featureValue.DescriptionEn = DescriptionEn;
            return featureValue;
        }
    }
}