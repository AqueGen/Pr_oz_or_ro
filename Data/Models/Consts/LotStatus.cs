namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус лота закупівлі
    /// </summary>
    public static class LotStatus
    {
        /// <summary>
        /// Активний лот закупівлі (активний)
        /// </summary>
        public const string ACTIVE = "active";

        /// <summary>
        /// Неуспішний лот закупівлі (не відбувся)
        /// </summary>
        public const string UNSUCCESSFUL = "unsuccessful";

        /// <summary>
        /// Завершено лот закупівлі (завершено)
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Скасовано лот закупівлі (скасовано)
        /// </summary>
        public const string CANCELLED = "cancelled";
    }
}