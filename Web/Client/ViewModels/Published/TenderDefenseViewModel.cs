using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class TenderDefenseViewModel : BaseTenderViewModel, ITenderDefenseViewModel
    {
        public TenderDefenseViewModel()
        {
        }

        public TenderDefenseViewModel(TenderDTO tender) : base(tender)
        {
            if (tender != null)
            {
                ProcurementMethodRationale = tender.ProcurementMethodRationale;
                TitleEn = tender.TitleEn;
                Features = tender.Features?.Select(m => new FeatureEUViewModel(tender.Guid, m));
                Lots = tender.Lots?.Select(m => new LotEUViewModel(tender.Guid, m));
                Questions = tender.Questions?.Select(m => new QuestionViewModel(tender.Guid, m));
                Complaints = tender.Complaints?.Select(m => new TenderComplaintViewModel(tender.Guid, m));
                Documents = tender.Documents?.Select(m => new DocumentViewModel(tender.Guid, m));
            }
        }

        public string ProcurementMethodRationale { get; set; }

        public override string ProcurementMethodType { get; set; } =
            Data.Models.Consts.ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE;

        public string TitleEn { get; set; }

        public IEnumerable<IFeatureEUViewModel> Features { get; set; }
        public IEnumerable<ILotEUViewModel> Lots { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<ContactViewModel> Contacts { get; set; }
        public IEnumerable<TenderComplaintViewModel> Complaints { get; set; }

        public IEnumerable<IDocumentViewModel> Documents { get; set; }


        public override TenderDTO ToDTO()
        {
            var tender = base.ToDTO();
            tender.ProcurementMethodRationale = ProcurementMethodRationale;
            tender.ProcurementMethodType = ProcurementMethodType;
            tender.TitleEn = TitleEn;
            return tender;
        }
    }
}