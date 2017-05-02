using System;
using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels
{
    public class FeatureEUViewModel : BaseFeatureViewModel, IFeatureEUViewModel
    {
        public FeatureEUViewModel(Guid tenderGuid, FeatureDTO feature) : base(tenderGuid, feature)
        {
            if (feature != null)
            {
                TitleEn = feature.TitleEn;
                DescriptionEn = feature.DescriptionEn;
                Values = feature.Values?.Select(m => new FeatureValueEUViewModel(tenderGuid, m)).ToList();
            }
        }

        public FeatureEUViewModel()
        {
        }

        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }

        public IEnumerable<IFeatureValueEUViewModel> Values { get; set; }

        public override FeatureDTO ToDTO()
        {
            var feature = base.ToDTO();
            feature.TitleEn = TitleEn;
            feature.DescriptionEn = DescriptionEn;
            return feature;
        }
    }
}