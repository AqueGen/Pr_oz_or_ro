using System;
using System.Collections.Generic;
using Kapitalist.Data.Models;

namespace Kapitalist.Data.Models.DTO
{
    public class TenderComplaintDTO : BaseComplaint
    {
        public Guid TenderGuid { get; set; }
        public string LotStringId { get; set; }
        //public int Id { get; set; }
        //public int TenderId { get; set; }
        public ICollection<TenderComplaintDocumentDTO> Documents { get; set; }
        //public int? LotId { get; set; }
        public OrganizationDTO Author { get; set; }
    }
}