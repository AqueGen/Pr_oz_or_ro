using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
    /// Тип скасування
    /// </summary>
    public enum CancellationType
    {
        /// <summary>
        /// закупівля
        /// </summary>
        [EnumMember(Value = "tender")]
        Tender = 0,

        /// <summary>
        /// лот
        /// </summary>
        [EnumMember(Value = "lot")]
        Lot = 1
    }
}