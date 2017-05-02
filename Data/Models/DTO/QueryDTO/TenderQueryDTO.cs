using Kapitalist.Data.Models;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO.QueryDTO
{
    public class TenderQueryDTO : BaseQueryDTO
    {

        public ICollection<string> ProcurementNumber { get; set; }
        public Period ApplicationsSubmissionPeriod { get; set; }
        public Period ClarificationPeriod { get; set; }
        public Period AuctionPeriod { get; set; }
        public Period QualificationPeriod { get; set; }

        public ICollection<string> Status { get; set; }
	}
}