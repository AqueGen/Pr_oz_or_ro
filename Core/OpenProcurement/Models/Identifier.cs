using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    public class Identifier : Kapitalist.Data.Models.BaseIdentifier
    {
        public Identifier()
        {
        }

        public Identifier(IIdentifier identifier)
            : base(identifier)
        {
        }
    }
}
