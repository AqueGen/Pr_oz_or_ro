using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
    public interface IDraftLot : IStringId, ITitled, IDraftAuctioned
    {
        string TitleEn { get; set; }

        string DescriptionEn { get; set; }
    }
}