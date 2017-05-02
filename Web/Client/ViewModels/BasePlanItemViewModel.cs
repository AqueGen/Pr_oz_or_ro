using System;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels
{
    public class BasePlanItemViewModel
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

        public BasePlanItemViewModel()
        {
        }

        public BasePlanItemViewModel(IDraftItem planItemDTO) : this()
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

        public BasePlanItemViewModel(Guid planGuid, IDraftItem planItemDTO) : this(planItemDTO)
        {
            PlanGuid = planGuid;
        }


    }
}