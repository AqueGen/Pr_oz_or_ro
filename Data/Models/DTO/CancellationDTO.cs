using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class CancellationDTO : BaseCancellation
    {
        public CancellationDTO()
        {
        }

        public CancellationDTO(ICancellation cancellation)
            : base(cancellation)
        {
        }
    }
}