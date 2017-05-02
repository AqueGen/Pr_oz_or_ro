using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Kapitalist.Data.Store.Models
{
    public class ProcuringEntity : Organization<ProcuringEntityIdentifier>, IProcuringEntity
    {
        public ProcuringEntity()
        {
        }

        public ProcuringEntity(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        public ProcuringEntityType? Kind { get; set; }

        public virtual Tender Tender { get; set; }

        [NotMapped]
        [Obsolete("Use ContactPoints instead")]
        public override ContactPoint ContactPoint
        {
            get {
                return ContactPoints?.OrderBy(c => c.SortingOrder)
                    .Select(c => new ContactPoint(c)).FirstOrDefault()
                    ?? new ContactPoint();
            }

            set {
                ContactPoints = new List<ProcuringEntityContactPoint>() {
                    new ProcuringEntityContactPoint(value)
                };
            }
        }

        public ICollection<ProcuringEntityContactPoint> ContactPoints { get; set; }
    }

    public class ProcuringEntityIdentifier : Identifier<ProcuringEntity>
    {
        public ProcuringEntityIdentifier()
        {
        }

        public ProcuringEntityIdentifier(IIdentifier identifier)
            : base(identifier)
        {
        }
    }

    public class ProcuringEntityContactPoint : ContactPointEx<ProcuringEntity>
    {
        public ProcuringEntityContactPoint()
        {
        }

        public ProcuringEntityContactPoint(ContactPoint contactPoint)
            : base(contactPoint)
        {
        }
    }
}