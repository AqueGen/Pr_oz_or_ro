namespace Kapitalist.Data.Models.Consts
{
    public static class ProcurementMethodType
    {
        /// <summary>
        /// оборонна закупівля
        /// </summary>
        public const string ABOVE_THRESHOLD_UA_DEFENSE = "aboveThresholdUA.defense";

        /// <summary>
        /// ідентифікатор процедури реєстрації угоди
        /// </summary>
        public const string REPORTING = "reporting";

        /// <summary>
        /// ідентифікатор переговорної процедури
        /// </summary>
        public const string NEGOTIATION = "negotiation";

        /// <summary>
        /// ідентифікатор скороченої переговорної процедури
        /// </summary>
        public const string NEGOTIATION_QUICK = "negotiation.quick";

        /// <summary>
        /// допорогова закупівля
        /// </summary>
        public const string BELOW_THRESHOLD = "belowThreshold";

        /// <summary>
        /// запорогова закупівля
        /// </summary>
        public const string ABOVE_THRESHOLD_UA = "aboveThresholdUA";

        /// <summary>
        /// міжнародна запорогова закупівля
        /// </summary>
        public const string ABOVE_THRESHOLD_EU = "aboveThresholdEU";
    }
}