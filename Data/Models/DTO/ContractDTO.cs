using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class ContractDTO : BaseContract
    {
        public ContractDTO()
        {
        }

        public ContractDTO(IContract contract)
            : base(contract)
        {
        }
    }
}