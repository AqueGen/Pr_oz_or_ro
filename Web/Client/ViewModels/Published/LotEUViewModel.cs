using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class LotEUViewModel : BaseLotViewModel, ILotEUViewModel
    {
        public LotEUViewModel()
        {
        }

        public LotEUViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public LotEUViewModel(Guid tenderGuid, LotDTO lotDTO) : base(tenderGuid, lotDTO)
        {
            if (lotDTO != null)
            {
                TitleEn = lotDTO.TitleEn;
                Items = lotDTO.Items?.Select(m => new ItemEUViewModel(tenderGuid, m));
                Features = lotDTO.Features?.Select(m => new FeatureEUViewModel(tenderGuid, m));
                Status = lotDTO.Status;
                AuctionPeriod = new PeriodViewModel(lotDTO.AuctionPeriod);
                AuctionUrl = lotDTO.AuctionUrl;
                Complaints = lotDTO.Complaints?.Select(m => new TenderComplaintViewModel(tenderGuid, m));
                Documents = lotDTO.Documents?.Select(m => new DocumentViewModel(tenderGuid, m));
            }
        }

        [Required]
        public string TitleEn { get; set; }

        public IEnumerable<IDocumentViewModel> Documents { get; set; }

        public string Status { get; set; }
        public string AuctionUrl { get; set; }
        public PeriodViewModel AuctionPeriod { get; set; }

        public IEnumerable<IItemEUViewModel> Items { get; set; }
        public IEnumerable<IFeatureEUViewModel> Features { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<TenderComplaintViewModel> Complaints { get; set; }

        public override LotDTO ToModelDTO()
        {
            var lot = base.ToModelDTO();
            lot.TitleEn = TitleEn;
            lot.Status = Status;
            lot.AuctionUrl = AuctionUrl;
            lot.AuctionPeriod = AuctionPeriod?.ToDTO();
            return lot;
        }
    }
}