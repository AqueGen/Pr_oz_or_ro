using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.ViewModels
{
    public class ContactPointViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string FaxNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Telephone { get; set; }

        [Url]
        public string Url { get; set; }

        public ContactPointViewModel()
        {
        }

        public ContactPointViewModel(ContactPoint dto)
        {
            if(dto != null)
            {
                Name = dto.Name;
                Telephone = dto.Telephone;
                Email = dto.Email;
                FaxNumber = dto.FaxNumber;
                Url = dto.Url;
            }
        }

        public ContactPoint ToDTO()
        {
            return new ContactPoint()
            {
                Name = Name,
                Telephone = Telephone,
                Email = Email,
                FaxNumber = FaxNumber,
                Url = Url
            };
        }
    }
}