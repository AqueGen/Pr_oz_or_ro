using System;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IBaseFeatureValueViewModel
    {
        string Description { get; set; }
        string FeatureStringId { get; set; }
        int Id { get; set; }
        Guid TenderGuid { get; set; }
        string Title { get; set; }
        float Value { get; set; }
    }
}