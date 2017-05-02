using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Mappers;

namespace Kapitalist.Web.Client.ViewModels
{
    public class ProcuringEntityViewModel
    {
        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        public ContactPointViewModel ContactPoint { get; set; }

        public IdentifierViewModel Identifier { get; set; }
        public ProcuringEntityType? Kind { get; set; }
        public string Name { get; set; }


        public ProcuringEntityViewModel()
        {
        }

        public ProcuringEntityViewModel(OrganizationDTO entityDTO)
        {
            if (entityDTO != null)
            {
                Name = entityDTO.Name;
                Kind = entityDTO.Kind;
                ContactPoint = new ContactPointViewModel(entityDTO.ContactPoint);
                Address = new AddressViewModel(entityDTO.Address);
                Identifier = new IdentifierViewModel(entityDTO.Identifier);
            }
        }

        public OrganizationDTO ToDTO()
        {
            return new OrganizationDTO
            {
                Name = Name,
                ContactPoint = ContactPoint.ToDTO(),
                Address = Address.ToDTO(),
                Kind = Kind
            };
        }
    }
}