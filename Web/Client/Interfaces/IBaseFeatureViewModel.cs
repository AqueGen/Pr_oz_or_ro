using System;
using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseFeatureViewModel
    {
        string Description { get; set; }
        string RelatedItem { get; set; }
        string StringId { get; set; }
        Guid TenderGuid { get; set; }
        string Title { get; set; }
        FeatureType Type { get; set; }
    }
}