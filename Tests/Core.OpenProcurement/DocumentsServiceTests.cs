using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Kapitalist.Core.OpenProcurement
{
    public abstract class DocumentsServiceTests<T> : ServiceTests<T>
        where T : IDocumentsService
    {
        public DocumentsServiceTests(T service)
            : base(service)
        {
        }
    }
}