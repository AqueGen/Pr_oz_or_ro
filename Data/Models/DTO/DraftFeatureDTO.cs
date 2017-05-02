using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class DraftFeatureDTO : BaseFeature
    {
        public DraftFeatureDTO()
        {
        }

        public DraftFeatureDTO(IFeature feature)
            : base(feature)
        {
        }

        public ICollection<FeatureValueDTO> Values { get; set; }

        //public int TenderId { get; set; }
        public Guid TenderGuid { get; set; }

        //public int Id { get; set; }
    }
}