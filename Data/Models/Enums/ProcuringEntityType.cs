using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
    /// <summary>
	/// Тип замовника
	/// </summary>
	public enum ProcuringEntityType
    {
        /// <summary>
        /// Замовник (загальний)
        /// </summary>
        [EnumMember(Value = "general")]
        General = 0,

        /// <summary>
        /// Замовник, що здійснює діяльність в окремих сферах господарювання
        /// </summary>
        [EnumMember(Value = "special")]
        Special = 1,

        /// <summary>
        /// Замовник, що здійснює закупівлі для потреб оборони
        /// </summary>
        [EnumMember(Value = "defense")]
        Defense = 2,

        /// <summary>
        /// Юридичні особи, які не є замовниками в розумінні Закону, 
        /// але є державними, комунальними, казенними підприємствами, господарськими товариствами чи об’єднаннями підприємств, 
        /// у яких державна чи комунальна частка складає 50 і більше відсотків
        /// </summary>
        [EnumMember(Value = "other")]
        Other = -1
    }
}
