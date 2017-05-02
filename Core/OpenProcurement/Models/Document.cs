using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    public class Document : BaseDocument
    {
        public Document()
        {
        }

        public Document(IDraftDocument document)
            : base(document)
        {
        }

        public Document(IDocument document)
            : base(document)
        {
        }
    }
}