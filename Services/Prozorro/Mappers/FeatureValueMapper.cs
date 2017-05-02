using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Store.Models;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class FeatureValueMapper
    {
        public static DraftFeatureValue ToDraft(this FeatureValueDTO source)
        {
            return source == null
                ? null
                : new DraftFeatureValue(source);
        }

        public static FeatureValueDTO ToDTO(this DraftFeatureValue source)
        {
            return source == null
                ? null
                : new FeatureValueDTO(source)
                {
                    Id = source.Id
                };
        }

        public static FeatureValueDTO ToDTO(this FeatureValue source)
        {
            return source == null
                ? null
                : new FeatureValueDTO(source)
                {
                    Id = source.Id
                };
        }

        public static Rest.FeatureValue ToRest(this DraftFeatureValue source)
        {
            return source == null
                ? null
                : new Rest.FeatureValue(source);
        }
    }
}