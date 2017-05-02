using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class TenderDTO : BaseTender
    {
        public TenderDTO()
        {
        }

        public TenderDTO(ITender tender)
            : base(tender)
        {
        }

        public OrganizationDTO ProcuringEntity { get; set; }

        public ICollection<LotDTO> Lots { get; set; }

        public ICollection<ItemDTO> Items { get; set; }

        public ICollection<FeatureDTO> Features { get; set; }

        public ICollection<DocumentDTO> Documents { get; set; }

        public ICollection<QuestionDTO> Questions { get; set; }

        public ICollection<ComplaintDTO> Complaints { get; set; }

        public ICollection<BidDTO> Bids { get; set; }

        public ICollection<AwardDTO> Awards { get; set; }

        public ICollection<ContractDTO> Contracts { get; set; }

        public ICollection<CancellationDTO> Cancellations { get; set; }
    }
}