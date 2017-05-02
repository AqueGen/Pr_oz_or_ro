using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.ViewModels.Base
{
    public abstract class BaseLimitedViewModel : BaseTenderViewModel, IBaseLimitedViewModel
    {
        public BaseLimitedViewModel()
        {
        }

        public BaseLimitedViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                Items = tender.Items?.Select(m => new ItemViewModel(tender.Guid, m));
                ProcurementMethodRationale = tender.ProcurementMethodRationale;
            }
        }

        public string ProcurementMethodRationale { get; set; }

        public IEnumerable<IItemViewModel> Items { get; set; }

        public override TenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodRationale = ProcurementMethodRationale;
            return tender;
        }
    }
}