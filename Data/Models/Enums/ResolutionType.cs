using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
    /// Тип вирішення
    /// </summary>
    public enum ResolutionType
    {
        /// <summary>
        /// недійсно
        /// </summary>
        [EnumMember(Value = "invalid")]
        Invalid = 0,

        /// <summary>
        /// вирішено
        /// </summary>
        [EnumMember(Value = "resolved")]
        Resolved = 1,

        /// <summary>
        /// відхилено
        /// </summary>
        [EnumMember(Value = "declined")]
        Declined = 2,
    }
}