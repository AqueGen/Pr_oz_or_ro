using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public abstract class BaseTenderViewModel : IBaseTenderViewModel
    {
        public BaseTenderViewModel()
        {
        }

        public BaseTenderViewModel(TenderDTO tender)
        {
            if (tender != null)
            {
                Guid = tender.Guid;
                Title = tender.Title;
                Description = tender.Description;
                Value = new ValueViewModel(tender.Value);
                Identifier = tender.Identifier;
                AuctionPeriod = new PeriodViewModel(tender.AuctionPeriod);
                AwardPeriod = new PeriodViewModel(tender.AwardPeriod);
                AuctionUrl = tender.AuctionUrl;
                Status = tender.Status;
                ProcuringEntity = new ProcuringEntityViewModel(tender.ProcuringEntity);
                AwardCriteria = tender.AwardCriteria;
            }
        }

        public string Description { get; set; }
        public Guid Guid { get; set; }
        public virtual string ProcurementMethodType { get; set; }
        public ProcuringEntityViewModel ProcuringEntity { get; set; }
        public string Title { get; set; }
        public ValueViewModel Value { get; set; }
        public PeriodViewModel AuctionPeriod { get; }
        public string AuctionUrl { get; set; }
        public PeriodViewModel AwardPeriod { get; }

        public string Identifier { get; set; }

        public string Status { get; set; }
        public string AwardCriteria { get; set; }


        public virtual TenderDTO ToDTO()
        {
            var tender = new TenderDTO
            {
                Description = Description,
                Guid = Guid,
                ProcurementMethodType = ProcurementMethodType,
                ProcuringEntity = ProcuringEntity?.ToDTO(),
                Title = Title,
                Value = Value.ToDTO(),
                AuctionPeriod = AuctionPeriod?.ToDTO(),
                AuctionUrl = AuctionUrl,
                AwardPeriod = AwardPeriod?.ToDTO(),
                Identifier = Identifier,
                Status = Status,
                AwardCriteria = AwardCriteria
            };
            return tender;
        }
    }
}