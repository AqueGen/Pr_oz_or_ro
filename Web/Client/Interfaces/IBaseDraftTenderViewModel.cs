using System;
using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseDraftTenderViewModel
    {
        string Description { get; set; }
        Guid Guid { get; set; }
        string ProcurementMethodType { get; set; }
        ProcuringEntityViewModel ProcuringEntity { get; set; }
        string Title { get; set; }
        ValueViewModel Value { get; set; }
        string AwardCriteria { get; set; }

    }
}