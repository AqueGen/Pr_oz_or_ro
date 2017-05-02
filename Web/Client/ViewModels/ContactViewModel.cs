using System;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
        }

        public ContactViewModel(Guid tenderGuid, ContactPointDTO contactPoint) : this(contactPoint)
        {
            TenderGuid = tenderGuid;
        }
        public ContactViewModel(ContactPointDTO contactPoint)
        {
            if (contactPoint != null)
            {
                Language = contactPoint.AvailableLanguage;
                Email = contactPoint.Email;
                FaxNumber = contactPoint.FaxNumber;
                Id = contactPoint.Id;
                Name = contactPoint.Name;
                NameEn = contactPoint.NameEn;
                Telephone = contactPoint.Telephone;
                Url = contactPoint.Url;
            }
        }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string FaxNumber { get; set; }

        public int Id { get; set; }

        public string Language { get; set; }

        public string Name { get; set; }
        public string NameEn { get; set; }

        [Phone]
        public string Telephone { get; set; }

        public Guid TenderGuid { get; set; }

        [Url]
        public string Url { get; set; }


        public ContactPointDTO ToDTO()
        {
            var contactDTO = new ContactPointDTO
            {
                AvailableLanguage = Language,
                Email = Email,
                FaxNumber = FaxNumber,
                Id = Id,
                Name = Name,
                NameEn = NameEn,
                Telephone = Telephone,
                Url = Url
            };
            return contactDTO;
        }
    }
}