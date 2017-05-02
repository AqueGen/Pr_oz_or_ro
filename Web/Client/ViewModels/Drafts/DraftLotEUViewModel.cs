using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftLotEUViewModel : BaseLotViewModel, IDraftLotEUViewModel, IValidatableObject
    {
        public DraftLotEUViewModel()
        {
        }

        public DraftLotEUViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public DraftLotEUViewModel(Guid tenderGuid, DraftLotDTO lotDTO) : base(tenderGuid, lotDTO)
        {
            if (lotDTO != null)
            {
                TitleEn = lotDTO.TitleEn;
                Items = lotDTO.Items?.Select(m => new DraftItemEUViewModel(tenderGuid, m));
                Features = lotDTO.Features?.Select(m => new FeatureEUViewModel(tenderGuid, m));
                Documents = lotDTO.Documents?.Select(m => new DraftDocumentViewModel(tenderGuid, m));
            }
        }

        [Required]
        public string TitleEn { get; set; }

        public IEnumerable<IDraftItemEUViewModel> Items { get; set; }
        public IEnumerable<IFeatureEUViewModel> Features { get; set; }
        public IEnumerable<IDraftDocumentViewModel> Documents { get; set; }

        public override DraftLotDTO ToDraftDTO()
        {
            var lot = base.ToDraftDTO();
            lot.TitleEn = TitleEn;
            return lot;
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext);
        }
    }
}