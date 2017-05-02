using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftLotViewModel : BaseLotViewModel, IDraftLotViewModel, IValidatableObject
    {
        public DraftLotViewModel()
        {
        }

        public DraftLotViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public DraftLotViewModel(Guid tenderGuid, DraftLotDTO lotDTO) : base(tenderGuid, lotDTO)
        {
            if (lotDTO != null)
            {
                Items = lotDTO.Items?.Select(m => new DraftItemViewModel(tenderGuid, m));
                Features = lotDTO.Features?.Select(m => new FeatureViewModel(tenderGuid, m));
                Documents = lotDTO.Documents?.Select(m => new DraftDocumentViewModel(tenderGuid, m));
            }
        }

        public IEnumerable<IDraftItemViewModel> Items { get; set; }
        public IEnumerable<IFeatureViewModel> Features { get; set; }
        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}