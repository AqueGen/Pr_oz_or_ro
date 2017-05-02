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
    public class Budget : IComplexType, IBudget
    {
        [JsonProperty("id")]
        [StringLength(64)]
        public string Id { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        /// <summary>
        /// OpenContracting Description: Кількість як число.
        /// Повинно бути додатнім.
        /// </summary>
        [JsonProperty("amountNet")]
        public decimal AmountNet { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Кількість як число.
        /// Повинно бути додатнім.
        /// </summary>
        [JsonRequired]
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Валюта у трибуквенному форматі ISO 4217.
        /// За замовчуванням = UAH
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty("currency")]
        [StringLength(3), Truncate]
        public string Currency { get; set; }

        /// <summary>
        /// Детальний опис буджету плану.
        /// </summary>
        [JsonRequired]
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("project")]
        public Project Project { get; set; }

        public Budget()
        {
        }

        public Budget(IBudget budget)
        {
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}
