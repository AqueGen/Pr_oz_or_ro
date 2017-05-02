using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Store.Models;
using System.Collections.Generic;
using System.Linq;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class UserOrganizationMapper
    {
        public static UserOrganization ToModel(this OrganizationDTO source)
        {
            if (source == null)
                return null;

            var userOrganization = new UserOrganization(source);
            if (source.Identifier != null || source.AdditionalIdentifiers?.Count > 0)
            {
                List<UserOrganizationIdentifier> allIdentifiers = new List<UserOrganizationIdentifier>();
                if (source.Identifier != null)
                {
                    allIdentifiers.Add(new UserOrganizationIdentifier(source.Identifier) { Main = true });
                }
                if (source.AdditionalIdentifiers != null)
                {
                    allIdentifiers.AddRange(source.AdditionalIdentifiers?.Select(i => new UserOrganizationIdentifier(i)));
                }
                userOrganization.AllIdentifiers = allIdentifiers;
            }

            return userOrganization.InitComplexProperties();
        }

        public static Rest.ProcuringEntity ToRest(this UserOrganization source)
        {
            return source == null
                ? null
                : new Rest.ProcuringEntity(source)
                {
                    Identifier = source.Identifier.ToRest(),
                    AdditionalIdentifiers = source.AdditionalIdentifiers?.Select(i => i.ToRest()).ToArray()
                }.DropComplexProperties();
        }
    }
}