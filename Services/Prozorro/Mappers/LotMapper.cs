using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Store.Models;
using System.Linq;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class LotMapper
    {
        public static DraftLotDTO ToDTO(this DraftLot source)
        {
            return source == null
                ? null
                : new DraftLotDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    Items = source.Items
                        .Select(m => m.ToDTO())
                        .ToList(),
                    Features = source.Tender.Features
                        .Where(m => m.FeatureType == FeatureType.Lot && m.RelatedItem == source.StringId)
                        .Select(m => m.ToDTO())
                        .ToList(),
                    Documents = source.Tender.Documents?
                        .Where(m => m.DocumentOf == RelatedTo.Lot && m.RelatedId == source.StringId)
                        .Select(x => x.ToDTO())
                        .ToList()
                };
        }

        public static LotDTO ToDTO(this Lot source)
        {
            return source == null
                ? null
                : new LotDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    Complaints = source.Complaints?
                        .Where(m => m.Tender.Guid == source.Tender.Guid && m.Lot.StringId == source.StringId)
                        .Select(x => x.ToDTO()).ToList(),
                    Items = source.Items?.Select(x => x.ToDTO()).ToList(),
                    Features = source.Tender.Features?
                        .Where(m => m.FeatureType == FeatureType.Lot && m.RelatedItem == source.StringId)
                        .Select(m => m.ToDTO()).ToList(),
                    Questions = source.Tender.Questions?
                        .Where(m => m.QuestionOf == RelatedTo.Lot && m.RelatedItem == source.StringId)
                        .Select(x => x.ToDTO()).ToList(),
                };
        }

        public static Rest.Lot ToRest(this DraftLot source)
        {
            return source == null
                ? null
                : new Rest.Lot(source).DropComplexProperties();
        }

        public static DraftLot ToDraft(this DraftLotDTO source)
        {
            return source == null
                ? null
                : new DraftLot(source).InitComplexProperties();
        }
    }
}