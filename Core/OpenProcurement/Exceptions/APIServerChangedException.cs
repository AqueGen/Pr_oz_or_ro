using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
    public class APIServerChangedException : APIException
    {
        public APIServerChangedException()
            : base("Claster's server changed!", 
                  new APIStatusCodeException(System.Net.HttpStatusCode.PreconditionFailed, "Precondition Failed"))
        {
        }
    }
}
