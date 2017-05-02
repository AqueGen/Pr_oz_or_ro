using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public class BaseFeatureViewModel : IBaseFeatureViewModel
    {
        public Guid TenderGuid { get; set; }

        public string StringId { get; set; }

        [Required]
        public FeatureType Type { get; set; }

        [Required]
        public string RelatedItem { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }



        public BaseFeatureViewModel(Guid tenderGuid, FeatureDTO feature)
        {
            TenderGuid = tenderGuid;
            if (feature != null)
            {
                Title = feature.Title;
                Type = feature.FeatureType;
                Description = feature.Description;
                RelatedItem = feature.RelatedItem;
                StringId = feature.StringId;
            }
        }

        public BaseFeatureViewModel()
        {
        }

        public virtual FeatureDTO ToDTO()
        {
            var featureDTO = new FeatureDTO
            {
                Description = Description,
                Title = Title,
                FeatureType = Type,
                RelatedItem = RelatedItem,
                StringId = StringId
            };
            if (string.IsNullOrWhiteSpace(featureDTO.StringId))
            {
                featureDTO.StringId = Guid.NewGuid().ToString("N");
            }
            return featureDTO;
        }
    }
}