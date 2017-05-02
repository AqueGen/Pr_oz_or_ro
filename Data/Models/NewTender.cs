using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models
{
    public class NewTender : INewTender
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IProcuringEntity ProcuringEntity { get; set; }
        public IValue Value { get; set; }
        public Period EnquiryPeriod { get; set; }
        public Period TenderPeriod { get; set; }
        public IValue MinimalStep { get; set; }
    }
}