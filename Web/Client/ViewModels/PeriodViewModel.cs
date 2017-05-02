using System;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models;

namespace Kapitalist.Web.Client.ViewModels
{
    public class PeriodViewModel
    {
        [DisplayFormat(DataFormatString = "dd.MM.yyyy HH:mm", ApplyFormatInEditMode = true)]
        [UIHint("DateTime")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "dd.MM.yyyy HH:mm", ApplyFormatInEditMode = true)]
        [UIHint("DateTime")]
        public DateTime? EndDate { get; set; }

        public PeriodViewModel(Period period)
        {
            StartDate = period.StartDate;
            EndDate = period.EndDate;
        }

        public PeriodViewModel(EnquiryPeriod period)
        {
            StartDate = period.StartDate;
            EndDate = period.EndDate;
        }

        public PeriodViewModel()
        {
        }

        public virtual Period ToDTO()
        {
            return new Period
            {
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}