using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class DraftLotDTO : BaseDraftLot
    {
        public DraftLotDTO()
        {
        }

        public DraftLotDTO(IDraftLot lot)
            : base(lot)
        {
        }

        public Guid TenderGuid { get; set; }

        public ICollection<ComplaintDTO> Complaints { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
        public ICollection<FeatureDTO> Features { get; set; }
        public ICollection<DraftTenderDocumentDTO> Documents { get; set; }
    }
}