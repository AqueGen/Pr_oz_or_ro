using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;
using Kapitalist.Data.Models.Enums;
using System.Linq;

namespace Kapitalist.Data.Models.DTO
{
    public class OrganizationDTO : BaseOrganization, IProcuringEntity
    {
        public OrganizationDTO()
        {
        }

        public OrganizationDTO(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        public ProcuringEntityType? Kind { get; set; }

        public IdentifierDTO Identifier { get; set; }

        public ICollection<IdentifierDTO> AdditionalIdentifiers { get; set; }

        public override ContactPoint ContactPoint
        {
            get {
                return ContactPoints?.Select(c => new ContactPoint(c)).FirstOrDefault();
            }

            set {
                ContactPoints = value == null
                    ? null
                    : new List<ContactPointDTO>() { new ContactPointDTO(value) };
            }
        }

        public ICollection<ContactPointDTO> ContactPoints { get; set; }
    }
}