namespace Kapitalist.Data.Models.Interfaces
{
    public interface IFeature : IStringId, ITitled
    {
        /// <summary>
        /// Обов'язково!
        /// Тип нецінового критерію
        /// </summary>
        FeatureType FeatureType { get; set; }

        /// <summary>
        /// Id пов’язаного елемента.
        /// </summary>
        string RelatedItem { get; set; }

        string TitleEn { get; set; }

        string DescriptionEn { get; set; }
    }
}