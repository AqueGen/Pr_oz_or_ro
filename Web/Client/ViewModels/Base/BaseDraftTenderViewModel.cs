using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;

namespace Kapitalist.Web.Client.ViewModels.Base
{

    public abstract class BaseDraftTenderViewModel : IBaseDraftTenderViewModel, IValidatableObject
    {
        public BaseDraftTenderViewModel()
        {
        }

        public BaseDraftTenderViewModel(DraftTenderDTO tender)
        {
            if (tender != null)
            {
                Guid = tender.Guid;
                Title = tender.Title;
                Description = tender.Description;
                Value = new ValueViewModel(tender.Value);
                ProcuringEntity = new ProcuringEntityViewModel(tender.ProcuringEntity);
                AwardCriteria = tender.AwardCriteria;
            }
        }

        public string Description { get; set; }

        public abstract string ProcurementMethodType { get; set; }

        public Guid Guid { get; set; }

        public ProcuringEntityViewModel ProcuringEntity { get; set; }

        [Required]
        public string Title { get; set; }

        public ValueViewModel Value { get; set; }
        public string AwardCriteria { get; set; }


        public virtual DraftTenderDTO ToDTO()
        {
            var tender = new DraftTenderDTO();
            tender.Guid = Guid == Guid.Empty ? Guid.NewGuid() : Guid;
            tender.Title = Title;
            tender.Description = Description;
            tender.Value = Value?.ToDTO();
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.AwardCriteria = AwardCriteria;
            return tender;
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}