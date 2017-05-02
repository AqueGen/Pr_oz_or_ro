using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class DocumentViewModel : IDocumentViewModel
    {
        public DocumentViewModel()
        {
        }

        public DocumentViewModel(Guid tenderGuid, DocumentDTO documentDTO)
        {
            TenderGuid = tenderGuid;
            if (documentDTO != null)
            {
                StringId = documentDTO.StringId;
                Description = documentDTO.Description;
                Title = documentDTO.Title;
                Format = documentDTO.Format;
                DocumentOf = documentDTO.DocumentOf;
                DocumentType = documentDTO.DocumentType;
                Language = documentDTO.Language;
                RelatedId = documentDTO.RelatedItem;
            }
        }

        public Guid TenderGuid { get; set; }

        public string StringId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumentType { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }
        public RelatedTo DocumentOf { get; set; }
        public string RelatedId { get; set; }

        public HttpPostedFileBase Document { get; set; }


        public DocumentDTO ToDTO()
        {
            var documentDTO = new DocumentDTO
            {
                StringId = StringId,
                Description = Description,
                Title = Title,
                Format = Format,
                DocumentOf = DocumentOf,
                DocumentType = DocumentType,
                Language = Language,
                RelatedItem = RelatedId
            };


            if (string.IsNullOrWhiteSpace(documentDTO.StringId))
            {
                documentDTO.StringId = Guid.NewGuid().ToString("N");
            }

            return documentDTO;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((Document == null) || (Document?.ContentLength <= 0))
            {
                yield return new ValidationResult("Докумет повинен бути присутній.", new[] {"Document"});
            }
        }
    }
}