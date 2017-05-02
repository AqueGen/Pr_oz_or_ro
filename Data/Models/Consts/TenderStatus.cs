namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
	/// Статус Закупівлі.
	/// </summary>
	public static class TenderStatus
    {
        /// <summary>
        /// Чернетка закупівлі
        /// </summary>
        public const string DRAFT = "draft";
        
        /// <summary>
        /// Активна закупівля
        /// </summary>
        public const string ACTIVE = "active";

        /// <summary>
        /// Період уточнень (уточнення)
        /// </summary>
        public const string ACTIVE_ENQUIRIES = "active.enquiries";

        /// <summary>
        /// Очікування пропозицій (пропозиції)
        /// </summary>
        public const string ACTIVE_TENDERING = "active.tendering";

        /// <summary>
        /// Період аукціону (аукціон)
        /// </summary>
        public const string ACTIVE_AUCTION = "active.auction";

        /// <summary>
        /// Попередня кваліфікація переможця (пре-кваліфікація)
        /// </summary>
        public const string ACTIVE_PRE_QUALIFICATION = "active.pre-qualification";

        /// <summary>
        /// active.pre-qualification.stand-still
        /// </summary>
        public const string ACTIVE_PRE_QUALIFICATION_STAND_STILL = "active.pre-qualification.stand-still";

        /// <summary>
        /// Кваліфікація переможця (кваліфікація)
        /// </summary>
        public const string ACTIVE_QUALIFICATION = "active.qualification";

        /// <summary>
        /// Пропозиції розглянуто (розглянуто)
        /// </summary>
        public const string ACTIVE_AWARDED = "active.awarded";

        /// <summary>
        /// Закупівля не відбулась (не відбулась)
        /// </summary>
        public const string UNSUCCESSFUL = "unsuccessful";

        /// <summary>
        /// Завершена закупівля (завершена)
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Відмінена закупівля (відмінена)
        /// </summary>
        public const string CANCELLED = "cancelled";
    }
}