using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class CancelTenderViewModel
    {
        public CancelTenderViewModel()
        {
        }

        public CancelTenderViewModel(Guid tenderGuid)
        {
            TenderGuid = tenderGuid;
        }

        public string Description { get; set; }

        public HttpPostedFileBase DescriptionUploadFile { get; set; }
        public Guid TenderGuid { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DescriptionUploadFile == null) || (DescriptionUploadFile?.ContentLength <= 0))
            {
                yield return new ValidationResult("Докумет повинен бути присутній.", new[] {"DescriptionUploadFile"});
            }
        }
    }
}