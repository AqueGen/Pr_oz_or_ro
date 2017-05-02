namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
	/// Статус плану закупівлі.
	/// </summary>
	public static class PlanTenderStatus
    {
        /// <summary>
        /// Активна закупівля (за замовчуванням)
        /// </summary>
        public const string ACTIVE = "active";

        /// <summary>
        /// Завершена закупівля
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Відмінена закупівля
        /// </summary>
        public const string CANCELLED = "cancelled";

        /// <summary>
        /// Закупівля не відбулась
        /// </summary>
        public const string UNSUCCESSFUL = "unsuccessful";
    }
}
