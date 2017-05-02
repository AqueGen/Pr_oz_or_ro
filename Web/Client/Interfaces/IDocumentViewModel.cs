using System;
using System.Web;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IDocumentViewModel
    {
        string Description { get; set; }
        HttpPostedFileBase Document { get; set; }
        RelatedTo DocumentOf { get; set; }
        string DocumentType { get; set; }
        string Format { get; set; }
        string Language { get; set; }
        string RelatedId { get; set; }
        string StringId { get; set; }
        Guid TenderGuid { get; set; }
        string Title { get; set; }
    }
}