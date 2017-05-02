using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Store.Models;
using System.Linq;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class TenderMapper
    {
        public static DraftTenderDTO ToDTO(this DraftTender source)
        {
            return source == null
                ? null
                : new DraftTenderDTO(source)
                {
                    Lots = source.Lots?.Select(x => x.ToDTO()).ToList(),
                    Items = source.Items?.Select(x => x.ToDTO()).ToList(),
                    Features =
                        source.Features?.Where(m => m.FeatureType == FeatureType.Tender).Select(x => x.ToDTO()).ToList(),
                    Documents =
                        source.Documents?.Where(m => m.DocumentOf == RelatedTo.Tender).Select(x => x.ToDTO()).ToList(),
                    ProcuringEntity = source.ProcuringEntity.ToDTO(),
                    Contacts = source.ContactPointRefs.Select(m => m.ContactPoint.ToDTO())
                };
        }

        public static Rest.Tender ToRest(this DraftTender source)
        {
            if (source == null)
                return null;

            var tender = new Rest.Tender(source)
            {
                ProcuringEntity = source.ProcuringEntity.ToRest(),
                Lots = source.Lots?.Select(x => x.ToRest()).ToArray(),
                Items = source.Items?.Select(x => x.ToRest()).ToArray(),
                Features = source.Features?.Select(x => x.ToRest()).ToArray(),
                Documents = source.Documents?.Select(x => x.ToRest()).ToArray()
            }.DropComplexProperties();

            return tender;
        }

        public static TenderDTO ToDTO(this Tender source)
        {
            return source == null
                ? null
                : new TenderDTO(source)
                {
                    ProcuringEntity = source.ProcuringEntity.ToDTO(),
                    Lots = source.Lots?.Select(x => x.ToDTO()).ToList(),
                    Items = source.Items?.Where(m => m.LotId == null).Select(x => x.ToDTO()).ToList(),
                    Features =
                        source.Features?.Where(m => m.FeatureType == FeatureType.Tender).Select(x => x.ToDTO()).ToList(),
                    Documents = source.Documents?.Select(x => x.ToDTO()).ToList(),
                    Bids = source.Bids?.Select(x => x.ToDTO()).ToList(),
                    Questions =
                        source.Questions?.Where(m => m.QuestionOf == RelatedTo.Tender).Select(x => x.ToDTO()).ToList(),
                    Complaints =
                        source.Complaints?.Where(m => m.Tender.Guid == source.Guid && m.Lot == null)
                            .Select(x => x.ToDTO())
                            .ToList(),
                    Awards = source.Awards?.Select(x => x.ToDTO()).ToList(),
                    Contracts = source.Contracts?.Select(x => x.ToDTO()).ToList(),
                    Cancellations = source.Cancellations?.Select(x => x.ToDTO()).ToList(),
                };
        }

        public static DraftTender ToDraft(this DraftTenderDTO source)
        {
            return source == null
                ? null
                : new DraftTender(source)
                {
                    Lots = source.Lots?.Select(x => x.ToDraft()).ToList(),
                    Items = source.Items?.Select(x => x.ToDraft()).ToList(),
                    Features = source.Features?.Select(x => x.ToDraft()).ToList(),
                    Documents = source.Documents?.Select(x => x.ToDraft()).ToList()
                }.InitComplexProperties();
        }
    }
}