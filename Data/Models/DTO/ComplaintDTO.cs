using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class ComplaintDTO : BaseComplaint
    {
        public ComplaintDTO()
        {
        }

        public ComplaintDTO(IComplaint complaint)
            : base(complaint)
        {
        }

        public ICollection<DocumentDTO> Documents { get; set; }
        public Guid TenderGuid { get; set; }
        public string LotStringId { get; set; }
    }
}