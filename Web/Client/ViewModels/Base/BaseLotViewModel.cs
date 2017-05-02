using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public class BaseLotViewModel : IBaseLotViewModel
    {
        public BaseLotViewModel()
        {
        }

        public BaseLotViewModel(Guid tenderGuid) : this()
        {
            TenderGuid = tenderGuid;
        }

        public BaseLotViewModel(Guid tenderGuid, LotDTO lotDTO) : this(tenderGuid)
        {
            if (lotDTO != null)
            {
                StringId = lotDTO.StringId;
                Title = lotDTO.Title;
                Description = lotDTO.Description;
                Value = new ValueViewModel(lotDTO.Value);
                Guarantee = new GuaranteeViewModel(lotDTO.Guarantee);
                MinimalStep = new MinimalStepViewModel(Value.Amount, lotDTO.MinimalStep);
            }
        }

        public BaseLotViewModel(Guid tenderGuid, DraftLotDTO lotDTO) : this(tenderGuid)
        {
            if (lotDTO != null)
            {
                StringId = lotDTO.StringId;
                Title = lotDTO.Title;
                Description = lotDTO.Description;
                Value = new ValueViewModel(lotDTO.Value);
                Guarantee = new GuaranteeViewModel(lotDTO.Guarantee);
                MinimalStep = new MinimalStepViewModel(Value.Amount, lotDTO.MinimalStep);
            }
        }

        public Guid TenderGuid { get; set; }
        public string StringId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        public ValueViewModel Value { get; set; }
        public GuaranteeViewModel Guarantee { get; set; }
        public MinimalStepViewModel MinimalStep { get; set; }

        public virtual LotDTO ToModelDTO()
        {
            var lotDTO = new LotDTO
            {
                TenderGuid = TenderGuid,
                StringId = StringId,
                Description = Description,
                Title = Title,
                Value = Value.ToDTO(),
                Guarantee = Guarantee.ToDTO(),
                MinimalStep = new Value
                {
                    Amount = MinimalStep.Amount,
                    Currency = Value.Currency,
                    VATIncluded = Value.VATIncluded
                }
            };
            if (string.IsNullOrEmpty(StringId))
            {
                lotDTO.StringId = Guid.NewGuid().ToString("N");
            }
            return lotDTO;
        }

        public virtual DraftLotDTO ToDraftDTO()
        {
            var lotDTO = new DraftLotDTO
            {
                TenderGuid = TenderGuid,
                StringId = StringId,
                Description = Description,
                Title = Title,
                Value = Value.ToDTO(),
                Guarantee = Guarantee.ToDTO(),
                MinimalStep = new Value
                {
                    Amount = MinimalStep.Amount,
                    Currency = Value.Currency,
                    VATIncluded = Value.VATIncluded
                }
            };
            if (string.IsNullOrEmpty(StringId))
            {
                lotDTO.StringId = Guid.NewGuid().ToString("N");
            }
            return lotDTO;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Value.Amount <= MinimalStep.Amount)
            {
                yield return new ValidationResult("Значення amount повинно бути меншим за Tender.value.amount",
                    new[] {"MinimalStep.Amount"});
            }


            if (!string.IsNullOrWhiteSpace(MinimalStep.Currency) && (MinimalStep.Currency != Value.Currency))
            {
                yield return
                    new ValidationResult(
                        "Значення currency повинно бути або відсутнім, або співпадати з Tender.value.currency",
                        new[] {"MinimalStep.Currency"});
            }

            if (Value.VATIncluded != MinimalStep.VATIncluded)
            {
                yield return
                    new ValidationResult(
                        "Значення valueAddedTaxIncluded повинно бути або відсутнім, або співпадати з Tender.value.valueAddedTaxIncluded",
                        new[] {"MinimalStep.VATIncluded"});
            }
        }
    }
}