using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels
{
    public class PlanTenderViewModel
    {
        public string Status { get; set; }
        public string ProcurementMethod { get; set; }
        public string ProcurementMethodRationale { get; set; }
        public string ProcurementMethodType { get; set; }
        public PeriodViewModel TenderPeriod { get; set; }

        public PlanTenderViewModel()
        {
        }

        public PlanTenderViewModel(PlanTender planTender)
        {
            Status = planTender.Status;
            ProcurementMethod = planTender.ProcurementMethod;
            ProcurementMethodRationale = planTender.ProcurementMethodRationale;
            ProcurementMethodType = planTender.ProcurementMethodType;
            TenderPeriod = new PeriodViewModel(new Period
            {
                StartDate = planTender.TenderPeriod.StartDate,
                EndDate = planTender.TenderPeriod.EndDate
            });
        }

        public PlanTender ToDTO()
        {
            return new PlanTender
            {
                Status = Status,
                TenderPeriod = new Period(TenderPeriod.ToDTO().StartDate, TenderPeriod.ToDTO().EndDate),
                ProcurementMethodRationale = ProcurementMethodRationale,
                ProcurementMethod = ProcurementMethod,
                ProcurementMethodType = ProcurementMethodType
            };
        }
    }
}