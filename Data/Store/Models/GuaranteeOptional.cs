using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	[ComplexType]
	internal class GuaranteeOptional
	{
		public decimal? Amount { get; set; }

		[StringLength(3), Truncate]
		public string Currency { get; set; }

		public static implicit operator Guarantee(GuaranteeOptional guarantee)
		{
			return guarantee.Amount.HasValue
				? new Guarantee { Amount = guarantee.Amount.Value, Currency = guarantee.Currency }
				: null;
		}

		public static implicit operator GuaranteeOptional(Guarantee guarantee)
		{
			return guarantee == null
				? new GuaranteeOptional()
				: new GuaranteeOptional { Amount = guarantee.Amount, Currency = guarantee.Currency };
		}
	}
}
