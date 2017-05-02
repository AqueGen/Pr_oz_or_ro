using System;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class DraftTenderDocumentDTO : BaseDocument
    {
        public DraftTenderDocumentDTO()
        {
        }

        public DraftTenderDocumentDTO(IDraftDocument document)
            : base(document)
        {
        }

        public Guid TenderGuid { get; set; }
        public byte[] Data { get; set; }
    }
}