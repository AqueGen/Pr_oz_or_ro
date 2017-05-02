using System;
using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseLotViewModel
    {
        string Description { get; set; }
        GuaranteeViewModel Guarantee { get; set; }
        MinimalStepViewModel MinimalStep { get; set; }
        string StringId { get; set; }
        Guid TenderGuid { get; set; }
        string Title { get; set; }
        ValueViewModel Value { get; set; }
    }
}