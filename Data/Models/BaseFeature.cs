using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Неціновий критерій
    /// </summary>
    public abstract class BaseFeature : IFeature
    {
        public BaseFeature()
        {
        }

        public BaseFeature(IFeature feature)
        {
            StringId = feature.StringId;
            Title = feature.Title;
            TitleEn = feature.TitleEn;
            Description = feature.Description;
            DescriptionEn = feature.DescriptionEn;
            FeatureType = feature.FeatureType;
            RelatedItem = feature.RelatedItem;
        }

        /// <summary>
		/// Код нецінового критерію.
		/// </summary>
        [JsonProperty("code", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [Required]
        public string StringId { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Назва критерію.
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        /// <summary>
        /// Опис критерію.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_en")]
        public string DescriptionEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Тип нецінового критерію
        /// </summary>
        [JsonRequired]
        [JsonProperty("featureOf")]
        public FeatureType FeatureType { get; set; }

        /// <summary>
        /// Id пов’язаного елемента.
        /// </summary>
        [JsonProperty("relatedItem")]
        [StringLength(64)]
        public string RelatedItem { get; set; }
    }

    /// <summary>
    /// Тип нецінового критерію
    /// </summary>
    public enum FeatureType
    {
        /// <summary>
        /// учасник закупівлі
        /// </summary>
        [EnumMember(Value = "tender")]
        Tender = 0,

        /// <summary>
        /// учасник закупівлі
        /// </summary>
        [EnumMember(Value = "tenderer")]
        Tenderer = 0,

        /// <summary>
        /// учасник лоту
        /// </summary>
        [EnumMember(Value = "lot")]
        Lot = 1,

        /// <summary>
        /// учасник item
        /// </summary>
        [EnumMember(Value = "item")]
        Item = 2
    }
}