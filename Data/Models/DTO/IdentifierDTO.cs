using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class IdentifierDTO : BaseIdentifier
    {
        public IdentifierDTO()
        {
        }

        public IdentifierDTO(IIdentifier identifier)
            : base(identifier)
        {
        }
    }
}