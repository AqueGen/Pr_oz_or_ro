using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftDocumentViewModel : IDraftDocumentViewModel
    {
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

        public DraftDocumentViewModel()
        {
        }

        public DraftDocumentViewModel(Guid tenderGuid, DraftTenderDocumentDTO draftTenderDocumentDTO)
        {
            TenderGuid = tenderGuid;
            if(draftTenderDocumentDTO != null)
            {
                StringId = draftTenderDocumentDTO.StringId;
                Description = draftTenderDocumentDTO.Description;
                Title = draftTenderDocumentDTO.Title;
                Format = draftTenderDocumentDTO.Format;
                DocumentOf = draftTenderDocumentDTO.DocumentOf;
                DocumentType = draftTenderDocumentDTO.DocumentType;
                Language = draftTenderDocumentDTO.Language;
                RelatedId = draftTenderDocumentDTO.RelatedItem;
            }
        }
        public DraftDocumentViewModel(Guid tenderGuid, DocumentDTO tenderDocumentDTO)
        {
            TenderGuid = tenderGuid;
            if(tenderDocumentDTO != null)
            {
                StringId = tenderDocumentDTO.StringId;
                Description = tenderDocumentDTO.Description;
                Title = tenderDocumentDTO.Title;
                Format = tenderDocumentDTO.Format;
                DocumentOf = tenderDocumentDTO.DocumentOf;
                DocumentType = tenderDocumentDTO.DocumentType;
                Language = tenderDocumentDTO.Language;
                RelatedId = tenderDocumentDTO.RelatedItem;
            }
        }
        

        public DraftTenderDocumentDTO ToDTO()
        {
            var documentDTO = new DraftTenderDocumentDTO
            {
                StringId = StringId,
                Description = Description,
                Title = Title,
                Format = Format,
                DocumentOf = DocumentOf,
                DocumentType = DocumentType,
                Language = Language,
                RelatedItem = RelatedId,
                TenderGuid = TenderGuid
            };
            var target = new MemoryStream();
            Document.InputStream.CopyTo(target);
            documentDTO.Data = target.ToArray();


            if (string.IsNullOrWhiteSpace(documentDTO.StringId))
            {
                documentDTO.StringId = Guid.NewGuid().ToString("N");
            }

            return documentDTO;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Document == null || Document?.ContentLength <= 0)
            {
                yield return new ValidationResult("Докумет повинен бути присутній.", new[] { "Document" });
            }
        }
    }
}