using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Kapitalist.Data.Models.DTO
{
    public class ItemDTO : BaseItem
    {
        public ItemDTO()
        {
        }

        public ItemDTO(IDraftItem item)
            : base(item)
        {
        }

        public ItemDTO(IItem item)
            : base(item)
        {
        }

        public ICollection<FeatureDTO> Features { get; set; }

        public ClassificationDTO Classification { get; set; }

        public ICollection<ClassificationDTO> AdditionalClassifications { get; set; }

        public string LotStringId { get; set; }
        public Guid TenderGuid { get; set; }
        public string ContractStringId { get; set; }
        public string AwardStringId { get; set; }
        public ICollection<DocumentDTO> Documents { get; set; }
        public ICollection<QuestionDTO> Questions { get; set; }
    }
}