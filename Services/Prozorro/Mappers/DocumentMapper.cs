using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Store.Models;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class DocumentMapper
    {
        public static DraftTenderDocumentDTO ToDTO(this DraftTenderDocument source)
        {
            return source == null
                ? null
                : new DraftTenderDocumentDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    RelatedItem = source.RelatedId,
                    Data = source.Data
                };
        }

        public static DraftTenderDocument ToDraft(this DraftTenderDocumentDTO source)
        {
            return source == null
                ? null
                : new DraftTenderDocument(source)
                {
                    RelatedId = source.RelatedItem,
                    Data = source.Data
                };
        }

        public static DocumentDTO ToDTO(this IDocument source)
        {
            return source == null
                ? null
                : new DocumentDTO(source);
        }

        public static Rest.Document ToRest(this DraftTenderDocument source)
        {
            return source == null
                ? null
                : new Rest.Document(source)
                {
                    RelatedItem = source.RelatedId
                };
        }
    }
}