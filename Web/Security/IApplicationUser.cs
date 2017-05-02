using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Web.Security
{
    public interface IApplicationUser
    {
        int UserOrganizationId { get; }
    }
}
