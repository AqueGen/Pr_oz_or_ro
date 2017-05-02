using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Resources;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels
{
    public class AddressViewModel : IAddressViewModel, IEmpty
    {
        public string Country { get; set; } = GlobalRes.Ukraine;

        public string Locality { get; set; }

        public string PostalCode { get; set; }

        public string Region { get; set; }

        public string Street { get; set; }

        public AddressViewModel(IAddress dto)
        {
            if (dto != null)
            {
                Country = dto.CountryName;
                Locality = dto.Locality;
                PostalCode = dto.PostalCode;
                Region = dto.Region;
                Street = dto.StreetAddress;
            }
        }

        public AddressViewModel()
        {
        }


        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Country)
                   || string.IsNullOrWhiteSpace(Locality)
                   || string.IsNullOrWhiteSpace(PostalCode)
                   || string.IsNullOrWhiteSpace(Region)
                   || string.IsNullOrWhiteSpace(Street);
        }
    }
}