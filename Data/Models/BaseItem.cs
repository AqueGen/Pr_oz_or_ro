using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models
{
    public abstract class BaseItem : BaseDraftItem, IItem
    {
        public BaseItem()
        {
        }

        public BaseItem(IDraftItem item)
            : base(item)
        {
        }

        public BaseItem(IItem item)
            : base(item)
        {
        }
    }
}