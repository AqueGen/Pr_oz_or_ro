using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
    /// ��� ��������
    /// </summary>
    public enum ResolutionType
    {
        /// <summary>
        /// �������
        /// </summary>
        [EnumMember(Value = "invalid")]
        Invalid = 0,

        /// <summary>
        /// �������
        /// </summary>
        [EnumMember(Value = "resolved")]
        Resolved = 1,

        /// <summary>
        /// ��������
        /// </summary>
        [EnumMember(Value = "declined")]
        Declined = 2,
    }
}