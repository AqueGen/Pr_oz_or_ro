using System;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.DTO
{
    public class TenderComplaintDocumentDTO
    {
        //public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string StringId { get; set; }
        public string DocumentType { get; set; }
        public string Format { get; set; }
        public string Language { get; set; }
        public RelatedTo DocumentOf { get; set; }
        public DateTime DateModified { get; set; }
        public string RelatedItem { get; set; }
        public DateTime DatePublished { get; set; }
        public string Url { get; set; }
        //public int ComplaintId { get; set; }
        public string ComplaintStringId { get; set; }
    }
}