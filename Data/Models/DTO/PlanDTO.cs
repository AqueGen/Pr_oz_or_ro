using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class PlanDTO : BasePlan
    {
        public PlanDTO()
        {
        }

        public PlanDTO(IPlan plan)
            : base(plan)
        {
        }

        //public int Id { get; set; }
        public ClassificationDTO Classification { get; set; }

        public ICollection<ItemDTO> Items { get; set; }
        public ICollection<PlanClassificationDTO> AdditionalClassifications { get; set; }
        public PlanProcuringEntityDTO ProcuringEntity { get; set; }
        public DateTime DateSynced { get; set; }
    }
}