using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels
{
    public class AddContactViewModel
    {
        private Task<IEnumerable<ContactPointDTO>> contacts;

        public AddContactViewModel()
        {
        }

        public AddContactViewModel(Guid tenderGuid)
        {
            TenderGuid = tenderGuid;
        }

        public AddContactViewModel(Guid tenderGuid, IEnumerable<ContactViewModel> contactPoints, int contactPointId = 0) : this(tenderGuid)
        {
            ContactId = contactPointId;
            ContactPointSelectedList = new List<SelectListItem>();
            ContactPointSelectedList.AddRange(contactPoints.Select(m => new SelectListItem
            {
                Text = ContactToString(m),
                Value = m.Id.ToString(),
                Selected = m.Id == contactPointId
            }));
        }


        [Required]
        public int ContactId { get; set; }
        public List<SelectListItem> ContactPointSelectedList { get; set; }
        public Guid TenderGuid { get; set; }

        private string ContactToString(ContactViewModel contact)
        {
            return
                $"{contact.Name}, {contact.NameEn},{contact.Email}, {contact.Language}, {contact.Url}, {contact.Telephone}, {contact.FaxNumber}";
        }
    }
}