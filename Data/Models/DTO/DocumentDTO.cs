using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models.DTO
{
    public class DocumentDTO : BaseDocument
    {
        public DocumentDTO()
        {
        }

        public DocumentDTO(IDocument document)
            : base(document)
        {
        }
    }
}