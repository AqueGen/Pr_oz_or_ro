using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Kapitalist.Data.Store.Models
{
    public class UserOrganization : Organization<UserOrganizationIdentifier>, IProcuringEntity
    {
        public UserOrganization()
        {
        }

        public UserOrganization(IOrganization organization)
            : base(organization)
        {
            Kind = (organization as IProcuringEntity)?.Kind;
        }

        public ProcuringEntityType? Kind { get; set; }

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
                ContactPoints = new List<UserOrganizationContactPoint>() {
                    new UserOrganizationContactPoint(value)
                };
            }
        }

        public ICollection<UserOrganizationContactPoint> ContactPoints { get; set; }
    }

    public class UserOrganizationIdentifier : Identifier<UserOrganization>
    {
        public UserOrganizationIdentifier()
        {
        }

        public UserOrganizationIdentifier(IIdentifier identifier)
            : base(identifier)
        {
        }
    }

    public class UserOrganizationContactPoint : ContactPointEx<UserOrganization>
    {
        public UserOrganizationContactPoint()
        {
        }

        public UserOrganizationContactPoint(ContactPoint contactPoint)
            : base(contactPoint)
        {
        }
    }
}