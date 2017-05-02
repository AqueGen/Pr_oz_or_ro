namespace Kapitalist.Data.Models.Interfaces
{
    public interface IFeatureValue : ITitled
    {
        /// <summary>
        /// Обов'язково!
        /// Значення критерію.
        /// </summary>
        float Value { get; set; }

        string TitleEn { get; set; }
    }
}