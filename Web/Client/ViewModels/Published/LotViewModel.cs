using System;
using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class LotViewModel : BaseLotViewModel, ILotViewModel
    {
        public LotViewModel()
        {
        }

        public LotViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public LotViewModel(Guid tenderGuid, LotDTO lotDTO) : base(tenderGuid, lotDTO)
        {
            if (lotDTO != null)
            {
                Items = lotDTO.Items?.Select(m => new ItemViewModel(tenderGuid, m));
                Features = lotDTO.Features?.Select(m => new FeatureViewModel(tenderGuid, m));
                Status = lotDTO.Status;
                AuctionPeriod = new PeriodViewModel(lotDTO.AuctionPeriod);
                AuctionUrl = lotDTO.AuctionUrl;
                Complaints = lotDTO.Complaints?.Select(m => new TenderComplaintViewModel(tenderGuid, m));
                Documents = lotDTO.Documents?.Select(m => new DocumentViewModel(tenderGuid, m));
            }
        }

        public string Status { get; set; }
        public IEnumerable<IDocumentViewModel> Documents { get; set; }
        public string AuctionUrl { get; set; }
        public PeriodViewModel AuctionPeriod { get; set; }

        public IEnumerable<IItemViewModel> Items { get; set; }
        public IEnumerable<IFeatureViewModel> Features { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<TenderComplaintViewModel> Complaints { get; set; }

        public override LotDTO ToModelDTO()
        {
            var lotDTO = base.ToModelDTO();
            lotDTO.Status = Status;
            lotDTO.AuctionUrl = AuctionUrl;
            lotDTO.AuctionPeriod = AuctionPeriod?.ToDTO();
            return lotDTO;
        }
    }
}