using System;
using System.Collections.Generic;
using Kapitalist.Web.Framework.Enums;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface INewTender
    {
        string Title { get; set; }
        string Description { get; set; }

        IProcuringEntity ProcuringEntity { get; set; }

        IValue Value{ get; set; }

        Period EnquiryPeriod { get; set; }
        Period TenderPeriod { get; set; }
        IValue MinimalStep { get; set; }
    }


    public interface INewItem
    {
        string Description { get; set; }

        string ClassificationScheme { get; set; }
        string ClassificationId { get; set; }
        string ClassificationDescription { get; set; }


        string AdditionalClassificationScheme { get; set; }
        string AdditionalClassificationId { get; set; }
        string AdditionalClassificationDescription { get; set; }

        string UnitCode { get; set; }
        string UnitName { get; set; }


        string Quantity { get; set; }
        DateTime DeliveryPeriodStart { get; set; }
        DateTime DeliveryPeriodEnd { get; set; }

    }
}
