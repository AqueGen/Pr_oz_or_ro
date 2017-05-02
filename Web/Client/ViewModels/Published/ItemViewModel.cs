using System;
using System.Collections.Generic;
using System.Linq;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Client.Interfaces;
using Kapitalist.Web.Client.ViewModels.Base;

namespace Kapitalist.Web.Client.ViewModels.Published
{
    public class ItemViewModel : BaseItemViewModel, IItemViewModel
    {
        public ItemViewModel()
        {
        }

        public ItemViewModel(Guid tenderGuid) : base(tenderGuid)
        {
        }

        public ItemViewModel(Guid tenderGuid, ItemDTO item) : base(tenderGuid, item)
        {
            if (item != null)
            {
                Features = item.Features?.Select(m => new FeatureViewModel(tenderGuid, m));
                Documents = item.Documents?.Select(m => new DocumentViewModel(tenderGuid, m));
                Questions = item.Questions?.Select(m => new QuestionViewModel(tenderGuid, m));
            }
        }

        public IEnumerable<QuestionViewModel> Questions { get; set; }
        public IEnumerable<IFeatureViewModel> Features { get; set; }
        public IEnumerable<IDocumentViewModel> Documents { get; set; }
    }
}