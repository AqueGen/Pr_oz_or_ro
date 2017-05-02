using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kapitalist.Web.Client.ViewModels.Plans
{
    public class PlanQueryViewModel : BaseQueryViewModel
    {
        public List<string> PlanNumbers { get; set; }
        public PeriodViewModel ProcedurePeriod { get; set; }
        public List<string> ProcedureType { get; set; }

        public string ToHttpDictionary()
        {
            List<KeyValuePair<string, object>> args = new List<KeyValuePair<string, object>>();

            if (Keyword != null) foreach (var item in Keyword) args.Add(new KeyValuePair<string, object>(nameof(Keyword), item));
            if (CpvCode != null) foreach (var item in CpvCode) args.Add(new KeyValuePair<string, object>(nameof(CpvCode), item));
            if (GsinCode != null) foreach (var item in GsinCode) args.Add(new KeyValuePair<string, object>(nameof(GsinCode), item));
            if (PlanNumbers != null) foreach (var item in PlanNumbers) args.Add(new KeyValuePair<string, object>(nameof(PlanNumbers), item));
            if (Procurer != null) foreach (var item in Procurer) args.Add(new KeyValuePair<string, object>(nameof(Procurer), item));
            if (Region != null) foreach (var item in Region) args.Add(new KeyValuePair<string, object>(nameof(Region), item));
            if (ProcedureType != null) foreach (var item in ProcedureType) args.Add(new KeyValuePair<string, object>(nameof(ProcedureType), item));

            if (ProcedurePeriod?.StartDate != null) args.Add(new KeyValuePair<string, object>(
                nameof(ProcedurePeriod) + "." + nameof(ProcedurePeriod.StartDate), ProcedurePeriod.StartDate));
            if (ProcedurePeriod?.EndDate != null) args.Add(new KeyValuePair<string, object>(
                 nameof(ProcedurePeriod) + "." + nameof(ProcedurePeriod.EndDate), ProcedurePeriod.EndDate));

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