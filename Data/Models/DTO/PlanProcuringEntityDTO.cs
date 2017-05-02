using System.Collections.Generic;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;

namespace Kapitalist.Data.Models.DTO
{
    public class PlanProcuringEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public ICollection<PlanProcuringEntityIdentifierDTO> AllIdentifiers { get; set; }
        public ContactPoint ContactPoint { get; set; }
        public ProcuringEntityType? Kind { get; set; }
    }
}