using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO.QueryDTO
{
    public class BaseQueryDTO
    {
        public List<string> Keyword { get; set; }
        public List<string> CpvCode { get; set; }
        public List<string> ScgsCode { get; set; }

        public List<string> Procurer { get; set; }
        public List<string> Region { get; set; }
    }
}