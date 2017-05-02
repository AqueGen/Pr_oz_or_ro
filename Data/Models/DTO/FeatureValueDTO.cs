using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class FeatureValueDTO : BaseFeatureValue
    {
        public FeatureValueDTO()
        {
        }

        public FeatureValueDTO(IFeatureValue featureValue)
            : base(featureValue)
        {
        }

        public int Id { get; set; }
        public string DescriptionEn { get; set; }
    }
}