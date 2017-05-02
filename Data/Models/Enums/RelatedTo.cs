using System.Runtime.Serialization;

namespace Kapitalist.Data.Models.Enums
{
	public enum RelatedTo
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
		Lot = 1,

		/// <summary>
		/// item
		/// </summary>
		[EnumMember(Value = "item")]
		Item = 2
	}
}
