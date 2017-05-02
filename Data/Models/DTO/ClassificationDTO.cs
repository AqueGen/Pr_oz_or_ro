using System;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class ClassificationDTO : BaseClassification
    {
        public ClassificationDTO()
        {
        }

        public ClassificationDTO(IClassification classification)
            : base(classification)
        {
        }

        //public int InternalId { get; set; }
    }
}