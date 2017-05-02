using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Store.Models;
using System.Linq;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class FeatureMapper
    {
        public static DraftFeature ToDraft(this FeatureDTO source)
        {
            return source == null
                ? null
                : new DraftFeature(source)
                {
                    Values = source.Values?.Select(x => x.ToDraft()).ToList()
                };
        }

        public static FeatureDTO ToDTO(this DraftFeature source)
        {
            return source == null
                ? null
                : new FeatureDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    Values = source.Values?.Select(x => x.ToDTO()).ToList()
                };
        }

        public static FeatureDTO ToDTO(this Feature source)
        {
            return source == null
                ? null
                : new FeatureDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    Values = source.Values?.Select(x => x.ToDTO()).ToList()
                };
        }

        public static Rest.Feature ToRest(this DraftFeature source)
        {
            return source == null
                ? null
                : new Rest.Feature(source)
                {
                    Values = source.Values?.Select(x => x.ToRest()).ToArray()
                };
        }
    }
}