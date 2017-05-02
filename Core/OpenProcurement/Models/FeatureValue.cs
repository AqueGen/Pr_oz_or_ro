using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    public class FeatureValue : BaseFeatureValue
    {
        public FeatureValue()
        {
        }

        public FeatureValue(IFeatureValue featureValue)
            : base(featureValue)
        {
        }
    }
}
