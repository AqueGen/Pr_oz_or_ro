using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement.Interfaces;

namespace Kapitalist.Core.OpenProcurement
{
    public class ContractService : DocumentsService, IContractService
    {
        public ContractService(Guid tenderId, CookieContainer cookieContainer = null)
                : base("tenders/" + tenderId.ToString("N") + "/contracts", cookieContainer)
        {
        }
    }
}
