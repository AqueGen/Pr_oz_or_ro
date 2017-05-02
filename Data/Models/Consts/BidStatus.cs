namespace Kapitalist.Data.Models.Consts
{
    /// <summary>
    /// Статус ставки
    /// </summary>
    public static class BidStatus
    {
        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// пропозиція ще обробляється
        ///// </summary>
        //public const string PENDING = "pending";

        /// <summary>
        /// реєстрація
        /// </summary>
        public const string REGISTRATION = "registration";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// активна пропозиція
        ///// </summary>
        //public const string ACTIVE = "active";

        /// <summary>
        /// дійсна пропозиція
        /// </summary>
        public const string VALID_BID = "validBid";

        /// <summary>
        /// недійсна пропозиція
        /// </summary>
        public const string INVALID_BID = "invalidBid";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// недійсна пропозиція
        ///// </summary>
        //public const string INVALID = "invalid";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// невдала пропозиція
        ///// </summary>
        //public const string UNSUCCESSFUL = "unsuccessful";

        //// TODO Taras 5: recheck enum
        //// Цього значення немає в документації, але воно використовується в пісочниці
        ///// <summary>
        ///// видалена пропозиція
        ///// </summary>
        //public const string DELETED = "deleted";
    }
}