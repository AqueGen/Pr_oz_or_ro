using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class ItemMapper
    {
        public static ItemDTO ToDTO(this DraftItem source)
        {
            return source == null
                ? null
                : new ItemDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    LotStringId = source.Lot?.StringId,
                    Classification = source.Classification.ToDTO(),
                    AdditionalClassifications = source.AdditionalClassifications?.Select(x => x.ToDTO()).ToList(),
                    Features = source.Tender.Features?
                        .Where(m => m.FeatureType == FeatureType.Item && m.RelatedItem == source.StringId)
                        .Select(m => m.ToDTO())
                        .ToList(),
                    //Documents = source.Tender.Documents?
                    //    .Where(m => m.DocumentOf == RelatedTo.Item && m.RelatedId == source.StringId)
                    //    .Select(x => x.ToDTO())
                    //    .ToList()
                };
        }

        public static DraftItem ToDraft(this ItemDTO source)
        {
            return source == null
                ? null
                : new DraftItem(source)
                {
                    //TenderGuid = map.Tender.Guid,
                    //LotStringId = map.Lot?.StringId
                    Classification = source.Classification.ToDraft<ClassificationCPVOptional>(),
                    AdditionalClassifications =
                        source.AdditionalClassifications?.Select(x => x.ToDraft<DraftClassification>()).ToList()
                }.InitComplexProperties();
        }

        public static Rest.Item ToRest(this DraftItem source)
        {
            return source == null
                ? null
                : new Rest.Item(source)
                {
                    Classification = source.Classification.ToRest(),
                    AdditionalClassifications =
                        source.AdditionalClassifications?.Select(x => x.ToRest()).ToArray(),
                    RelatedLot = source.Lot?.StringId
                }.DropComplexProperties();
        }

        public static ItemDTO ToDTO(this Item source)
        {
            return source == null
                ? null
                : new ItemDTO(source)
                {
                    Classification = source.Classification.ToDTO(),
                    AdditionalClassifications = source.AdditionalClassifications?.Select(x => x.ToDTO()).ToList(),
                    Features = source.Tender.Features
                        .Where(m => m.FeatureType == FeatureType.Item && m.RelatedItem == source.StringId)
                        .Select(m => m.ToDTO()).ToList(),
                    Questions = source.Tender.Questions?
                        .Where(m => m.QuestionOf == RelatedTo.Item && m.RelatedItem == source.StringId)
                        .Select(x => x.ToDTO()).ToList(),
                };
        }
    }
}
