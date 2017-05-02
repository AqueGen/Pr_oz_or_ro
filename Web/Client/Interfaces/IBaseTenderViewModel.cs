using System;
using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseTenderViewModel
    {
        string Description { get; set; }
        Guid Guid { get; set; }
        string ProcurementMethodType { get; set; }
        ProcuringEntityViewModel ProcuringEntity { get; set; }
        string Title { get; set; }
        ValueViewModel Value { get; set; }

        PeriodViewModel AuctionPeriod { get; }
        string AuctionUrl { get; set; }
        PeriodViewModel AwardPeriod { get; }
        string Identifier { get; set; }
        string Status { get; set; }
        string AwardCriteria { get; set; }
    }
}