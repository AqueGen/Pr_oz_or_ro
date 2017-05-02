using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Web.Security
{
    public enum OrganizationRoles
    {
        /// <summary>
        /// Замовник. Організація, що може створювати закупівлі.
        /// </summary>
        [Description("Замовник. Організація, що може створювати закупівлі.")]
        ProcuringEntity,

        /// <summary>
        /// Постачальник. Організація, що може подавати пропозиції на існуючі замовлення.
        /// </summary>
        [Description("Постачальник. Організація, що може подавати пропозиції на існуючі замовлення.")]
        Procurer
    }
}
