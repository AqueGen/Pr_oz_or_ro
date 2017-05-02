using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kapitalist.Web.Client.ViewModels.Tenders
{
    public class TenderQueryViewModel : BaseQueryViewModel
    {
        public List<string> ProcurementNumber { get; set; }
        public PeriodViewModel ApplicationsSubmissionPeriod { get; set; }
        public PeriodViewModel ClarificationPeriod { get; set; }
        public PeriodViewModel AuctionPeriod { get; set; }

        public PeriodViewModel QualificationPeriod { get; set; }

        public List<string> Status { get; set; }

        public string ToHttpDictionary()
        {
            List<KeyValuePair<string, object>> args = new List<KeyValuePair<string, object>>();

            if (Keyword != null) foreach (var item in Keyword) args.Add(new KeyValuePair<string, object>(nameof(Keyword), item));
            if (CpvCode != null) foreach (var item in CpvCode) args.Add(new KeyValuePair<string, object>(nameof(CpvCode), item));
            if (GsinCode != null) foreach (var item in GsinCode) args.Add(new KeyValuePair<string, object>(nameof(GsinCode), item));
            if (ProcurementNumber != null) foreach (var item in ProcurementNumber) args.Add(new KeyValuePair<string, object>(nameof(ProcurementNumber), item));
            if (Procurer != null) foreach (var item in Procurer) args.Add(new KeyValuePair<string, object>(nameof(Procurer), item));
            if (Region != null) foreach (var item in Region) args.Add(new KeyValuePair<string, object>(nameof(Region), item));
            if (Status != null) foreach (var item in Status) args.Add(new KeyValuePair<string, object>(nameof(Status), item));

            if (ApplicationsSubmissionPeriod?.StartDate != null) args.Add(new KeyValuePair<string, object>(
                nameof(ApplicationsSubmissionPeriod) + "." + nameof(ApplicationsSubmissionPeriod.StartDate), ApplicationsSubmissionPeriod.StartDate));
            if (ApplicationsSubmissionPeriod?.EndDate != null) args.Add(new KeyValuePair<string, object>(
                 nameof(ApplicationsSubmissionPeriod) + "." + nameof(ApplicationsSubmissionPeriod.EndDate), ApplicationsSubmissionPeriod.EndDate));

            if (ClarificationPeriod?.StartDate != null) args.Add(new KeyValuePair<string, object>(
                nameof(ClarificationPeriod) + "." + nameof(ClarificationPeriod.StartDate), ClarificationPeriod.StartDate));
            if (ClarificationPeriod?.EndDate != null) args.Add(new KeyValuePair<string, object>(
                 nameof(ClarificationPeriod) + "." + nameof(ClarificationPeriod.EndDate), ClarificationPeriod.EndDate));

            if (AuctionPeriod?.StartDate != null) args.Add(new KeyValuePair<string, object>(
                nameof(AuctionPeriod) + "." + nameof(AuctionPeriod.StartDate), AuctionPeriod.StartDate));
            if (AuctionPeriod?.EndDate != null) args.Add(new KeyValuePair<string, object>(
                 nameof(AuctionPeriod) + "." + nameof(AuctionPeriod.EndDate), AuctionPeriod.EndDate));

            if (QualificationPeriod?.StartDate != null) args.Add(new KeyValuePair<string, object>(
                nameof(QualificationPeriod) + "." + nameof(QualificationPeriod.StartDate), QualificationPeriod.StartDate));
            if (QualificationPeriod?.EndDate != null) args.Add(new KeyValuePair<string, object>(
                 nameof(QualificationPeriod) + "." + nameof(QualificationPeriod.EndDate), QualificationPeriod.EndDate));

            //args.Add(new KeyValuePair<string, object>(nameof(PageCount), PageCount));
            args.Add(new KeyValuePair<string, object>(nameof(PageSize), PageSize));

            var queryString = string.Join("&", args
                    .Where(x => x.Value != null)
                    .Select(x => x.Key + '=' +
                    HttpUtility.UrlEncode(x.Value is DateTime
                        ? ((DateTime)x.Value).ToString("s")
                        : x.Value.ToString())).ToArray());

            return queryString;
        }
    }
}