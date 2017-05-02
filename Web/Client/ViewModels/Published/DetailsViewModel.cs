using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class DetailsViewModel
    {
        public List<ItemViewModel> Items { get; set; }
        public List<LotViewModel> Lots { get; set; }
        public ProcuringEntityViewModel ProcuringEntity { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public TenderBeforeThresholdViewModel Tender { get; set; }
        public List<DocumentDTO> TenderDocuments { get; set; }

        public DetailsViewModel(TenderDTO tender)
        {
            if (tender != null)
            {
                Tender = new TenderBeforeThresholdViewModel(tender);
                if (tender.Documents != null)
                {
                    TenderDocuments = tender.Documents.ToList();
                }
                if (tender.ProcuringEntity != null)
                {
                    ProcuringEntity = new ProcuringEntityViewModel(tender.ProcuringEntity);
                }

                Lots = new List<LotViewModel>();
                foreach (var lot in tender.Lots)
                {
                    Lots.Add(new LotViewModel(tender.Guid, lot));
                }
                Items = new List<ItemViewModel>();
                foreach (var item in tender.Items)
                {
                    Items.Add(new ItemViewModel(tender.Guid, item));
                }

                Questions = new List<QuestionViewModel>();

                foreach (var item in tender.Questions)
                {
                    Questions.Add(new QuestionViewModel(tender.Guid, item));
                }


                //for (int index = Items.Count - 1; index >= 0; index--)
                //{
                //    var item = Items[index];
                //    if (!string.IsNullOrWhiteSpace(item.LotStringId))
                //    {
                //        var lot = Lots.FirstOrDefault(m => m.StringId == item.LotStringId);
                //        if (lot != null)
                //        {
                //            if (lot.Items == null)
                //            {
                //                lot.Items = new List<ItemViewModel>();
                //            }
                //            lot.Items.Add(item);
                //            Items.Remove(item);
                //        }
                //    }
                //}
            }
        }
    }
}