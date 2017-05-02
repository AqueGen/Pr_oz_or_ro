using System.Collections.Generic;

namespace Kapitalist.Web.Client.ViewModels
{
    public class BaseQueryViewModel
    {
        public List<string> Keyword { get; set; }

        public List<string> CpvCode { get; set; }
        public List<string> GsinCode { get; set; }

        public List<string> Procurer { get; set; }
        public List<string> Region { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageCount { get; set; }
        public int PageSize { get; set; } = 10;

    }
}