namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус скасування
    /// </summary>
    public static class CancellationStatus
    {
        /// <summary>
        /// За замовчуванням. Запит оформляється.
        /// </summary>
        public const string PENDING = "pending";

        /// <summary>
        /// Скасування активоване.
        /// </summary>
        public const string ACTIVE = "active";
    }
}