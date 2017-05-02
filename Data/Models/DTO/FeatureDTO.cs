using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class FeatureDTO : BaseFeature
    {
        public FeatureDTO()
        {
        }

        public FeatureDTO(IFeature feature)
            : base(feature)
        {
        }

        public ICollection<FeatureValueDTO> Values { get; set; }

        public Guid TenderGuid { get; set; }
    }
}