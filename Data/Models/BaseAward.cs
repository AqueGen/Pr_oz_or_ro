using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Рішення
    /// </summary>
    public abstract class BaseAward : IAward
    {
        public BaseAward()
        {
        }

        public BaseAward(IAward award)
        {
            StringId = award.StringId;
            Title = award.Title;
            Description = award.Description;
            Status = award.Status;
            Date = award.Date;
            Value = award.Value;
            ComplaintPeriod = award.ComplaintPeriod;
        }

        /// <summary>
        /// Генерується автоматично, лише для читання.
        /// OpenContracting Description: Ідентифікатор цього рішення.
        /// </summary>
        [Required]
        public string StringId { get; set; }

        /// <summary>
        /// OpenContracting Description: Назва рішення.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// OpenContracting Description: Опис рішення.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// OpenContracting Description: Поточний статус рішення, взятий зі списку кодів awardStatus.
        /// </summary>
        [JsonProperty("status")]
        [StringLength(32), Truncate]
        public string Status { get; set; }

        /// <summary>
        ///  Генерується автоматично, лише для читання.
        ///  OpenContracting Description: Дата рішення про підписання договору.
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Генерується автоматично, лише для читання.
        /// OpenContracting Description: Загальна вартість згідно цього рішення.
        /// </summary>
        [JsonProperty("value")]
        public Value Value { get; set; }

        /// <summary>
        /// Період часу, під час якого можна подавати скарги.
        /// </summary>
        [JsonProperty("complaintPeriod")]
        public Period ComplaintPeriod { get; set; }
    }
}
