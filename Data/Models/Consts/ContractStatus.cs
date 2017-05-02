namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус договору
    /// </summary>
    public static class ContractStatus
    {

        /// <summary>
        /// цей договір запропоновано, але він ще не діє. Можливо очікується його підписання.
        /// </summary>
        public const string PENDING = "pending";

        /// <summary>
        /// цей договір підписаний всіма учасниками, і зараз діє на законних підставах.
        /// </summary>
        public const string ACTIVE = "active";

        /// <summary>
        /// цей договір було скасовано до підписання.
        /// </summary>
        public const string CANCELLED = "cancelled";

        /// <summary>
        /// цей договір був підписаний та діяв, але вже завершився. 
        /// Це може бути пов’язано з виконанням договору, або з достроковим припиненням через якусь незавершеність.
        /// </summary>
        public const string TERMINATED = "terminated";
    }
}