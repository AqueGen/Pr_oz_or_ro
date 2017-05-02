namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус скарги
    /// </summary>
    public static class ComplaintStatus
    {
        /// <summary>
        /// чорновик, початковий етап
        /// </summary>
        public const string DRAFT = "draft";

        /// <summary>
        /// вимога
        /// </summary>
        public const string CLAIM = "claim";

        /// <summary>
        /// дано відповідь
        /// </summary>
        public const string ANSWERED = "answered";

        /// <summary>
        /// не вирішено, ще обробляється
        /// </summary>
        public const string PENDING = "pending";

        /// <summary>
        /// недійсно
        /// </summary>
        public const string INVALID = "invalid";

        /// <summary>
        /// відхилено
        /// </summary>
        public const string DECLINED = "declined";

        /// <summary>
        /// вирішено
        /// </summary>
        public const string RESOLVED = "resolved";

        /// <summary>
        /// відхилено
        /// </summary>
        public const string CANCELLED = "cancelled";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// зупиняється
        ///// </summary>
        //public const string STOPPING = "stopping";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// зупинено
        ///// </summary>
        //public const string STOPPED = "stopped";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// зупинено
        ///// </summary>
        //public const string SATISFIED = "satisfied";
    }
}