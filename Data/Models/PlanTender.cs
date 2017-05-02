using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
    [ComplexType]
    public class PlanTender : IComplexType, IPlanTender
    {
        /// <summary>
        /// Статус Закупівлі.
        /// </summary>
        [JsonProperty("status")]
        [StringLength(32), Truncate]
        public string Status { get; set; }

        /// <summary>
        /// генерується автоматично
        /// Метод закупівлі.
        /// </summary>
        [JsonProperty("procurementMethod")]
        [StringLength(32), Truncate]
        public string ProcurementMethod { get; set; }

        /// <summary>
        /// Обгрунтування використання такого методу закупівлі.
        /// </summary>
        [JsonProperty("procurementMethodRationale")]
        public string ProcurementMethodRationale { get; set; }

        /// <summary>
        /// Тип методу закупівлі.
        /// </summary>
        [JsonProperty("procurementMethodType")]
        [StringLength(32), Truncate]
        public string ProcurementMethodType { get; set; }

        /// <summary>
        /// Період, коли проводиться тендер.
        /// </summary>
        [JsonProperty("tenderPeriod")]
        public Period TenderPeriod { get; set; }

        public PlanTender()
        {
        }

        public PlanTender(IPlanTender planTender)
        {
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
