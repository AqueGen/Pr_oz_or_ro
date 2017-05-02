using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
    /// ��� ������
    /// </summary>
    public enum ComplaintType
    {
        /// <summary>
        /// ������
        /// </summary>
        [EnumMember(Value = "claim")]
        Claim = 0,

        /// <summary>
        /// ������
        /// </summary>
        [EnumMember(Value = "complaint")]
        �omplaint = 1
    }
}