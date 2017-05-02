using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class LotDTO : BaseLot
    {
        public LotDTO()
        {
        }

        public LotDTO(ILot lot)
           : base(lot)
        {
        }

        //public int Id { get; set; }
        //public int TenderId { get; set; }
        public Guid TenderGuid { get; set; }

        public ICollection<ComplaintDTO> Complaints { get; set; }
        public ICollection<ItemDTO> Items { get; set; }
        public ICollection<FeatureDTO> Features { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
        public ICollection<DocumentDTO> Documents { get; set; }

    }
}