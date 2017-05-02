using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.Mappers;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public class BaseItemViewModel : IBaseItemViewModel
    {
        public BaseItemViewModel()
        {
            Classification = new ClassificationViewModel();
            AdditionalClassifications = new List<ClassificationViewModel>();
        }

        public BaseItemViewModel(Guid tenderGuid) : this()
        {
            TenderGuid = tenderGuid;
        }

        public BaseItemViewModel(Guid tenderGuid, string lotId) : this(tenderGuid)
        {
            LotStringId = lotId;
        }

        public BaseItemViewModel(Guid tenderGuid, ItemDTO item) : this(tenderGuid)
        {
            if (item != null)
            {
                StringId = item.StringId;
                Description = item.Description;
                DeliveryAddress = new AddressViewModel(item.DeliveryAddress);
                DeliveryLocation = new DeliveryLocationViewModel(item.DeliveryLocation);
                DeliveryDate = new PeriodViewModel(item.DeliveryDate);
                Quantity = item.Quantity;
                Unit = new UnitViewModel(item.Unit);
                LotStringId = item.LotStringId;
                Classification = new ClassificationViewModel(item.Classification);
                AdditionalClassifications = item.AdditionalClassifications
                    ?.Select(m => new ClassificationViewModel(m)).ToList();
            }
        }

        public string ProcurementMethodType { get; set; }

        public List<ClassificationViewModel> AdditionalClassifications { get; set; }

        public string AdditionalClassificationsSelectedId
        {
            get
            {
                if (AdditionalClassifications != null)
                {
                    return string.Join(";", AdditionalClassifications.Select(m => $"{m.Id} - {m.Description}"));
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var gsins = value.Split(';');

                    if (AdditionalClassifications == null)
                    {
                        AdditionalClassifications = new List<ClassificationViewModel>();
                    }

                    foreach (var item in gsins)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            var gsinClassification = new ClassificationViewModel();
                            var array = item.Split(new[] {" - "}, StringSplitOptions.None);
                            if (array.Length == 2)
                            {
                                gsinClassification.Id = array[0];
                                gsinClassification.Description = array[1];
                                gsinClassification.Scheme = "ДКПП";
                            }
                            AdditionalClassifications.Add(gsinClassification);
                        }
                    }
                }
            }
        }

        public ClassificationViewModel Classification { get; set; }

        public string ClassificationSelectedId
        {
            get
            {
                if (Classification != null)
                {
                    return $"{Classification.Id} - {Classification.Description}";
                }
                return string.Empty;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (Classification == null)
                    {
                        Classification = new ClassificationViewModel();
                    }

                    var array = value.Split(new[] {" - "}, StringSplitOptions.None);
                    if (array.Length == 2)
                    {
                        Classification.Id = array[0];
                        Classification.Description = array[1];
                        Classification.Scheme = "CPV";
                    }
                }
            }
        }

        public AddressViewModel DeliveryAddress { get; set; }
        public PeriodViewModel DeliveryDate { get; set; }
        public DeliveryLocationViewModel DeliveryLocation { get; set; }

        [Required]
        public string Description { get; set; }


        public string LotStringId { get; set; }

        [Required]
        public long Quantity { get; set; }

        public string StringId { get; set; }


        public Guid TenderGuid { get; set; }

        [Required]
        public UnitViewModel Unit { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Classification.Scheme != "CPV" || Classification.IsEmpty())
            {
                yield return new ValidationResult("Класифікація classification.scheme обов’язково повинна бути CPV",
                    new[] { "Classification.Id" });
            }

            if (!AdditionalClassifications.Any(m => m.Scheme == "ДКПП"))
            {
                yield return new ValidationResult("Обов’язково мати хоча б один елемент з ДКПП у стрічці scheme",
                    new[] { "AdditionalClassifications" });
            }
            if (DeliveryAddress.IsEmpty() && DeliveryLocation.IsEmpty())
            {
                yield return new ValidationResult("Адреса або місце доставки повинні бути заповнені", new []
                {
                    "DeliveryAddress.Country",
                    "DeliveryAddress.Locality",
                    "DeliveryAddress.PostalCode",
                    "DeliveryAddress.Region",
                    "DeliveryAddress.Street",
                    "DeliveryLocation.Latitude",
                    "DeliveryLocation.Longitude",
                });
            }


        }

        public virtual ItemDTO ToDTO()
        {
            var dto = new ItemDTO
            {
                TenderGuid = TenderGuid,
                StringId = StringId,
                Classification = Classification.ToDTO(),
                Description = Description,
                AdditionalClassifications = AdditionalClassifications?.Select(m => m.ToDTO()).ToList(),
                Unit = Unit.ToDTO(),
                Quantity = Quantity,
                DeliveryDate = DeliveryDate.ToDTO(),
                DeliveryAddress = DeliveryAddress.ToDTO(),
                DeliveryLocation = DeliveryLocation.ToDTO(),
                LotStringId = LotStringId
            };
            if (string.IsNullOrWhiteSpace(StringId))
            {
                dto.StringId = Guid.NewGuid().ToString("N");
            }
            return dto;
        }
    }
}