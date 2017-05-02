using System;
using System.Collections.Generic;
using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseItemViewModel
    {
        List<ClassificationViewModel> AdditionalClassifications { get; set; }
        string AdditionalClassificationsSelectedId { get; set; }
        ClassificationViewModel Classification { get; set; }
        string ClassificationSelectedId { get; set; }
        AddressViewModel DeliveryAddress { get; set; }
        PeriodViewModel DeliveryDate { get; set; }
        DeliveryLocationViewModel DeliveryLocation { get; set; }
        string Description { get; set; }
        string LotStringId { get; set; }
        long Quantity { get; set; }
        string StringId { get; set; }
        Guid TenderGuid { get; set; }
        UnitViewModel Unit { get; set; }
        string ProcurementMethodType { get; set; }
    }
}