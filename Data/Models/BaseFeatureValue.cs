using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Значення критерію.
    /// </summary>
    public abstract class BaseFeatureValue : IFeatureValue
    {
        public BaseFeatureValue()
        {
        }

        public BaseFeatureValue(IFeatureValue featureValue)
        {
            Value = featureValue.Value;
            Title = featureValue.Title;
            TitleEn = featureValue.TitleEn;
            Description = featureValue.Description;
        }

        /// <summary>
        /// Обов'язково!
        /// Значення критерію.
        /// </summary>
        [JsonRequired]
        [JsonProperty("value")]
        public float Value { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Назва значення.
        /// </summary>
        [Required]
        [JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        /// <summary>
        /// Опис значення.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}