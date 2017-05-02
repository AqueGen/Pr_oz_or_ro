using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class BidDTO : BaseBid
    {
        public BidDTO()
        {
        }

        public BidDTO(IBid bid)
            : base(bid)
        {
        }
    }
}