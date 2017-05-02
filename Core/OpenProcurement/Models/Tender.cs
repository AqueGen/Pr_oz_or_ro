using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Закупівля.
    /// 
    /// Дати закупівлі повинні бути послідовними:
    /// Поточний час
    /// enquiryPeriod.startDate
    /// enquiryPeriod.endDate
    /// tenderPeriod.startDate
    /// tenderPeriod.endDate
    /// </summary>
    public class Tender : BaseTender
    {
        public Tender()
        {
        }

        public Tender(IDraftTender tender)
            : base(tender)
        {
        }

        public Tender(ITender tender)
            : base(tender)
        {
        }

        /// <summary>
        /// Обов'язково!
        /// Замовник (організація, що проводить закупівлю).
        /// OpenContracting Description: Об’єкт, що управляє закупівлею. Він не обов’язково є покупцем, який платить / використовує закуплені елементи.
        /// </summary>
        [JsonRequired]
        [JsonProperty("procuringEntity")]
        public ProcuringEntity ProcuringEntity { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Список, який містить елемент, що закуповується.
        /// OpenContracting Description: Товари та послуги, що будуть закуплені, поділені на спискові елементи, де це можливо.
        /// Елементи не повинні дублюватись, замість цього вкажіть кількість 2.
        /// </summary>
        [JsonRequired]
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        /// <summary>
        /// Властивості закупівлі.
        /// </summary>
        [JsonProperty("features")]
        public Feature[] Features { get; set; }

        /// <summary>
        /// OpenContracting Description: Всі документи та додатки пов’язані із закупівлею.
        /// </summary>
        [JsonProperty("documents")]
        public Document[] Documents { get; set; }

        /// <summary>
        /// Питання до замовника procuringEntity і відповіді на них.
        /// </summary>
        [JsonProperty("questions")]
        public Question[] Questions { get; set; }

        /// <summary>
        /// Скарги на умови закупівлі та їх вирішення.
        /// </summary>
        [JsonProperty("complaints")]
        public Complaint[] Complaints { get; set; }

        /// <summary>
        /// Список усіх пропозицій зроблених під час закупівлі разом із інформацією про учасників закупівлі, 
        /// їхні пропозиції та інша кваліфікаційна документація.
        /// OpenContracting Description: Список усіх компаній, які подали заявки для участі у закупівлі.
        /// </summary>
        [JsonProperty("bids")]
        public Bid[] Bids { get; set; }

        /// <summary>
        /// Усі кваліфікації (дискваліфікації та переможці).
        /// </summary>
        [JsonProperty("awards")]
        public Award[] Awards { get; set; }

        /// <summary>
        /// Список об’єктів Contract
        /// </summary>
        [JsonProperty("contracts")]
        public Contract[] Contracts { get; set; }

        /// <summary>
        /// Містить всі лоти закупівлі.
        /// </summary>
        [JsonProperty("lots")]
        public Lot[] Lots { get; set; }

        /// <summary>
        /// Містить 1 об’єкт зі статусом active на випадок, якщо закупівлю буде відмінено.
        /// Об’єкт Cancellation описує причину скасування закупівлі та надає відповідні документи, якщо такі є.
        /// </summary>
        [JsonProperty("cancellations")]
        public Cancellation[] Cancellations { get; set; }

        /// <summary>
        /// Генерується автоматично, лише для читання
        /// Зміни властивостей об’єктів Закупівлі
        /// </summary>
        [JsonProperty("revisions")]
        public Revision[] Revisions { get; set; }
    }
}
