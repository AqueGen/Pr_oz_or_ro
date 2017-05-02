using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels
{
    public class FeatureViewModel : BaseFeatureViewModel, IFeatureViewModel
    {
        public FeatureViewModel()
        {
        }

        public FeatureViewModel(Guid tenderGuid, FeatureDTO feature) : base(tenderGuid, feature)
        {
            if (feature != null)
            {
                Values = feature.Values?.Select(m => new FeatureValueViewModel(tenderGuid, m)).ToList();
            }
        }

        public IEnumerable<IBaseFeatureValueViewModel> Values { get; set; }
    }
}