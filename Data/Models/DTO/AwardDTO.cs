using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class AwardDTO : BaseAward
    {
        public AwardDTO()
        {
        }

        public AwardDTO(IAward award)
            : base(award)
        {
        }
    }
}