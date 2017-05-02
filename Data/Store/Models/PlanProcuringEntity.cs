using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Store.Models
{
    public class PlanProcuringEntity : BaseOrganization, IOrganization, IProcuringEntity
    {
        public PlanProcuringEntity()
        {
        }

        public PlanProcuringEntity(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        public int Id { get; set; }

        internal AddressOptional AddressOptional
        {
            get { return Address ?? new AddressOptional(); }
            set { Address = value.IsDefined() ? new Address(value) : null; }
        }

        internal ContactPointOptional ContactPointOptional
        {
            get { return ContactPoint ?? new ContactPointOptional(); }
            set { ContactPoint = value.IsEmpty() ? null : new ContactPoint(value); }
        }

        public virtual ICollection<PlanProcuringEntityIdentifier> AllIdentifiers { get; set; }

        [NotMapped]
        public PlanProcuringEntityIdentifier Identifier
        {
            get {
                return AllIdentifiers?.FirstOrDefault(i => i.Main);
            }
        }

        [NotMapped]
        public IEnumerable<PlanProcuringEntityIdentifier> AdditionalIdentifiers
        {
            get {
                IOrganizationIdentifier main = Identifier;
                return AllIdentifiers?.Where(i => i != main);
            }
        }

        public ProcuringEntityType? Kind { get; set; }

        public virtual Plan Plan { get; set; }
    }

    public class PlanProcuringEntityIdentifier : Identifier<PlanProcuringEntity>
    {
        public PlanProcuringEntityIdentifier()
        {
        }

        public PlanProcuringEntityIdentifier(IIdentifier identifier)
            : base(identifier)
        {
        }
    }
}