using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Framework.Attributes;
using Kapitalist.Web.Framework.Enums;

namespace Kapitalist.Web.Client.ViewModels.Profile
{
    public class PersonalViewModel
    {
        public PersonalViewModel()
        {
        }

        public PersonalViewModel(OrganizationDTO userOrganizationDTO)
        {
            if (userOrganizationDTO != null)
            {
                Company = new IdentifierViewModel(userOrganizationDTO.Identifier);
                Kind = userOrganizationDTO.Kind;
                ContactPoint = new ContactPointViewModel(userOrganizationDTO.ContactPoint);
                Address = new AddressViewModelRequired(userOrganizationDTO.Address);
                NameEn = userOrganizationDTO.NameEn;
                Name = userOrganizationDTO.Name;
            }
        }

        public AddressViewModelRequired Address { get; set; }

        public bool CaptchaSuccess { get; set; }

        [CheckIf("CompanyType", CompanyType.Corporation)]
        public IdentifierViewModel Company { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public CompanyType CompanyType { get; set; }

        public ContactPointViewModel ContactPoint { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //[Required]
        //public string FirstName { get; set; }

        [Required]
        public ProcuringEntityType? Kind { get; set; }

        //[Required]
        //public string LastName { get; set; }

        //[Required]
        //public string MiddleName { get; set; }



        [Phone]
        public string Phone { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameEn { get; set; }
    }


    public class AddressViewModelRequired : IAddressViewModel
    {
        [Required]
        public string Country { get; set; }

        [Required]
        public string Locality { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Region { get; set; }

        [Required]
        public string Street { get; set; }

        public AddressViewModelRequired()
        {
        }

        public AddressViewModelRequired(Address address)
        {
            Country = address.CountryName;
            Locality = address.Locality;
            PostalCode = address.PostalCode;
            Region = address.Region;
            Street = address.StreetAddress;
        }
    }
}