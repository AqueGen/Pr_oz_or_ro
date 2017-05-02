using System;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class DraftTenderDTO : BaseDraftTender
    {
        public DraftTenderDTO()
        {
        }

        public DraftTenderDTO(IDraftTender tender)
            : base(tender)
        {
        }

        public OrganizationDTO ProcuringEntity { get; set; }

        public ICollection<DraftLotDTO> Lots { get; set; }

        public ICollection<ItemDTO> Items { get; set; }

        public ICollection<FeatureDTO> Features { get; set; }
        public ICollection<DraftTenderDocumentDTO> Documents { get; set; }
        public int ProcuringEntityId { get; set; }
        public string Owner { get; set; }

        public IEnumerable<ContactPointDTO> Contacts{ get; set; }
    }
}