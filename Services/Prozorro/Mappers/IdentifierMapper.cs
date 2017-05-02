using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Store.Models;
using Rest = Kapitalist.Core.OpenProcurement.Models;

namespace Kapitalist.Services.Prozorro.Mappers
{
    public static class IdentifierMapper
    {
        public static IdentifierDTO ToDTO<T>(this Identifier<T> source)
            where T : class, IOrganization
        {
            return source == null
                ? null
                : new IdentifierDTO(source);
        }

        public static Rest.Identifier ToRest<T>(this Identifier<T> source)
            where T : class, IOrganization
        {
            return source == null
                ? null
                : new Rest.Identifier(source);
        }
    }
}