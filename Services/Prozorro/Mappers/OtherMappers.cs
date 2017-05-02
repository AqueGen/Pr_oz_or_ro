using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Store.Models;
using Kapitalist.Data.Models.DTO;
using System;
using System.Linq;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class OtherMappers
    {
        public static OrganizationDTO ToDTO(this ProcuringEntity source)
        {
            return source == null
                ? null
                : new OrganizationDTO(source)
                {
                    Identifier = source.Identifier.ToDTO(),
                    AdditionalIdentifiers = source.AdditionalIdentifiers?.Select(x => x.ToDTO()).ToList(),
                    ContactPoints = source.ContactPoints?.Select(x => x.ToDTO()).ToList()
                };
        }

        public static OrganizationDTO ToDTO<T>(this Organization<T> source)
            where T : class, IOrganizationIdentifier
        {
            return source == null
                ? null
                : new OrganizationDTO(source)
                {
                    Identifier = (source.Identifier as Identifier<IOrganization>).ToDTO(),
                    AdditionalIdentifiers =
                        source.AdditionalIdentifiers?.Select(x => (x as Identifier<IOrganization>).ToDTO()).ToList()
                };
        }

        public static ContactPointDTO ToDTO<T>(this ContactPointEx<T> source)
            where T : class, IOrganization
        {
            return source == null
                ? null
                : new ContactPointDTO(source)
                {
                    Id = source.Id
                };
        }

        public static BidDTO ToDTO(this Bid source)
        {
            return source == null
                ? null
                : new BidDTO(source)
                {
                };
        }

        public static QuestionDTO ToDTO(this Question source)
        {
            return source == null
                ? null
                : new QuestionDTO(source)
                {
                    Author = source.Author.ToDTO()
                };
        }

        public static ComplaintDTO ToDTO(this TenderComplaint source)
        {
            return source == null
                ? null
                : new ComplaintDTO(source)
                {
                    TenderGuid = source.Tender.Guid,
                    LotStringId = source.Lot?.StringId,
                    Documents = source.Documents?.Select(x => x.ToDTO()).ToList()
                };
        }

        public static ComplaintDTO ToDTO(this AwardComplaint source)
        {
            return source == null
                ? null
                : new ComplaintDTO(source)
                {
                    Documents = source.Documents?.Select(x => x.ToDTO()).ToList()
                };
        }

        public static AwardDTO ToDTO(this Award source)
        {
            return source == null
                ? null
                : new AwardDTO(source)
                {
                };
        }

        public static ContractDTO ToDTO(this Contract source)
        {
            return source == null
                ? null
                : new ContractDTO(source)
                {
                };
        }

        public static CancellationDTO ToDTO(this Cancellation source)
        {
            return source == null
                ? null
                : new CancellationDTO(source)
                {
                };
        }

        public static ProcuringEntity ToModel(this OrganizationDTO source)
        {
            throw new NotImplementedException();
            if (source == null)
                return null;
        }
    }
}