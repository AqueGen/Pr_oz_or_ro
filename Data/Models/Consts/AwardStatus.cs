namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус Рішення.
    /// </summary>
    public static class AwardStatus
    {
        /// <summary>
        /// Переможець розглядається кваліфікаційною комісією
        /// </summary>
        public const string PENDING = "pending";

        /// <summary>
        /// Кваліфікаційна комісія відмовила переможцю
        /// </summary>
        public const string UNSUCCESSFUL = "unsuccessful";

        /// <summary>
        /// закупівлю виграв учасник з пропозицією bid_id
        /// </summary>
        public const string ACTIVE = "active";

        /// <summary>
        /// Орган, що розглядає скарги, відмінив результати закупівлі
        /// </summary>
        public const string CANCELLED = "cancelled";
    }
}