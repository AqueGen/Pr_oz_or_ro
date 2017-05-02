using System;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Mappers;

namespace Kapitalist.Web.Client.ViewModels.Drafts
{
    public class DraftPlanItemViewModel
    {
        public Guid PlanGuid { get; set; }

        public ClassificationViewModel Classification { get; set; }
        public AddressViewModel DeliveryAddress { get; set; }
        public PeriodViewModel DeliveryDate { get; set; }
        public DeliveryLocationViewModel DeliveryLocation { get; set; }
        public string Description { get; set; }
        public long Quantity { get; set; }
        public string StringId { get; set; }
        public UnitViewModel Unit { get; set; }

        public DraftPlanItemViewModel()
        {
        }

        public DraftPlanItemViewModel(ItemDTO planItemDTO)
        {
            //Classification = new ClassificationViewModel(planItemDTO.Classification);
            DeliveryAddress = new AddressViewModel(planItemDTO.DeliveryAddress);
            DeliveryDate = new PeriodViewModel(planItemDTO.DeliveryDate);
            DeliveryLocation = new DeliveryLocationViewModel(planItemDTO.DeliveryLocation);
            Description = planItemDTO.Description;
            Quantity = planItemDTO.Quantity;
            StringId = planItemDTO.StringId;
            Unit = new UnitViewModel(planItemDTO.Unit);
        }

        public DraftPlanItemViewModel(Guid planGuid, ItemDTO planItemDTO) : this(planItemDTO)
        {
            PlanGuid = planGuid;
        }

        public ItemDTO ToDTO()
        {
            var dto = new ItemDTO
            {
                Classification = Classification.ToDTO(),
                Description = Description,
                DeliveryLocation = DeliveryLocation.ToDTO(),
                DeliveryAddress = DeliveryAddress.ToDTO(),
                Unit = Unit.ToDTO(),
                Quantity = Quantity,
                StringId = StringId,
                DeliveryDate = DeliveryDate.ToDTO()
            };

            if (string.IsNullOrWhiteSpace(dto.StringId))
            {
                dto.StringId = Guid.NewGuid().ToString("N");
            }
            return dto;
        }
    }
}