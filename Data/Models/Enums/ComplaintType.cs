using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
    /// Тип скарги
    /// </summary>
    public enum ComplaintType
    {
        /// <summary>
        /// вимога
        /// </summary>
        [EnumMember(Value = "claim")]
        Claim = 0,

        /// <summary>
        /// скарга
        /// </summary>
        [EnumMember(Value = "complaint")]
        Сomplaint = 1
    }
}