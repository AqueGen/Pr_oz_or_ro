using System;
using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class ItemEUViewModel : BaseItemViewModel, IItemEUViewModel
    {
        public ItemEUViewModel()
        {
        }

        public ItemEUViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public ItemEUViewModel(Guid tenderGuid, ItemDTO item) : base(tenderGuid, item)
        {
            if (item != null)
            {
                Features = item.Features?.Select(m => new FeatureEUViewModel(tenderGuid, m));
                Questions = item.Questions?.Select(m => new QuestionViewModel(tenderGuid, m));
                Documents = item.Documents?.Select(m => new DocumentViewModel(tenderGuid, m));
                DescriptionEn = item.DescriptionEn;
            }
        }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<IFeatureEUViewModel> Features { get; set; }
        public string DescriptionEn { get; set; }
        public IEnumerable<IDocumentViewModel> Documents { get; set; }

        public override ItemDTO ToDTO()
        {
            var item = base.ToDTO();
            item.DescriptionEn = DescriptionEn;
            return item;
        }
    }
}