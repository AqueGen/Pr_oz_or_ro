using Kapitalist.Data.Models;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO.QueryDTO
{
    public class PlanQueryDTO : BaseQueryDTO
    {
        public ICollection<string> PlanNumbers { get; set; }
        public Period ProcedurePeriod { get; set; }
        public ICollection<string> ProcedureType { get; set; }
	}
}