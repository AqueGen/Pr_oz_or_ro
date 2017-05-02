using Kapitalist.Data.Models.Drafts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models.Drafts
{
    /// <summary>
    /// Лот закупівлі
    /// </summary>
    public abstract class BaseDraftLot : IDraftLot
    {
        public BaseDraftLot()
        {
        }

        public BaseDraftLot(IDraftLot lot)
        {
            StringId = lot.StringId;
            Title = lot.Title;
            TitleEn = lot.TitleEn;
            Description = lot.Description;
            DescriptionEn = lot.DescriptionEn;
            Value = lot.Value;
            Guarantee = lot.Guarantee;
            MinimalStep = lot.MinimalStep;
        }

        /// <summary>
        /// Генерується автоматично.
        /// </summary>
        [Required]
        public string StringId { get; set; }

        /// <summary>
        /// Назва лота закупівлі.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        /// <summary>
        /// Детальний опис лота закупівлі.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_en")]
        public string DescriptionEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Повний доступний бюджет лота закупівлі.
        /// Цінові пропозиції, більші ніж value, будуть відхилені.
        /// </summary>
        [JsonRequired]
        [JsonProperty("value")]
        public Value Value { get; set; }

        /// <summary>
        /// Забезпечення тендерної пропозиції
        /// </summary>
        [NotMapped]
        [JsonProperty("guarantee")]
        public Guarantee Guarantee { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Мінімальний крок аукціону (редукціону). Правила валідації:
        /// - amount повинно бути меншим, ніж Lot.value.amount
        /// - currency повинно або бути відсутнім, або відповідати Lot.value.currency
        /// - valueAddedTaxIncluded повинно або бути відсутнім, або відповідати Lot.value.valueAddedTaxIncluded
        /// </summary>
        [JsonRequired]
        [JsonProperty("minimalStep")]
        public Value MinimalStep { get; set; }
    }
}