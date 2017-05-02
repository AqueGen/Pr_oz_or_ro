using Kapitalist.Data.Models;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.Mappers
{
    public static class ViewModelMapper
    {
        public static Address ToDTO(this IAddressViewModel source)
        {
            return source == null
                ? null
                : new Address
                {
                    Locality = source.Locality,
                    PostalCode = source.PostalCode,
                    Region = source.Region,
                    CountryName = source.Country,
                    StreetAddress = source.Street
                };
        }
    }
}