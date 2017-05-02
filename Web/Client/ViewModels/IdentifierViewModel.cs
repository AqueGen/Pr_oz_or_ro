using Kapitalist.Data.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Web.Client.ViewModels
{
    public class IdentifierViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string LegalName { get; set; }

        [Required]
        public string LegalNameEn { get; set; }

        [Required]
        public string Scheme { get; set; }

        [Url]
        public string Uri { get; set; }

        public IdentifierViewModel()
        {
        }

        public IdentifierViewModel(IdentifierDTO identifierDTO)
        {
            if (identifierDTO != null)
            {
                Id = identifierDTO.Id;
                LegalName = identifierDTO.LegalName;
                Scheme = identifierDTO.Scheme;
                Uri = identifierDTO.Uri;
                LegalNameEn = identifierDTO.LegalNameEn;
            }
        }
    }
}