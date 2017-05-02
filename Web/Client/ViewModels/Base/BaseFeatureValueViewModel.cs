using System;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public class BaseFeatureValueViewModel : IBaseFeatureValueViewModel
    {
        public Guid TenderGuid { get; set; }
        public string FeatureStringId { get; set; }

        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public float Value { get; set; }

        public BaseFeatureValueViewModel()
        {
        }

        public BaseFeatureValueViewModel(Guid tenderGuid, FeatureValueDTO featureValueDTO)
        {
            if (featureValueDTO != null)
            {
                Description = featureValueDTO.Description;
                TenderGuid = tenderGuid;
                Id = featureValueDTO.Id;
                Title = featureValueDTO.Title;
                Value = featureValueDTO.Value;
            }
        }

        public virtual FeatureValueDTO ToDTO()
        {
            return new FeatureValueDTO
            {
                Description = Description,
                Title = Title,
                Value = Value,
                Id = Id
            };
        }
    }
}