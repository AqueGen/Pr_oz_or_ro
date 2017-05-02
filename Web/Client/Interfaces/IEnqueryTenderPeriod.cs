using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Interfaces
{
    public interface IEnqueryTenderPeriod
    {
        PeriodViewModel EnquiryPeriod { get; set; }
        PeriodViewModel TenderPeriod { get; set; }
    }
}