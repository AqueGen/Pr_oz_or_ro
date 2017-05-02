using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class Contract : BaseContract, IContract
	{
		public Contract()
		{
		}

		public Contract(IContract contract)
			: base (contract)
		{
			if (Value == null)
				Value = new Value();
			if (Period == null)
				Period = new Period();
		}

		public int Id { get; set; }

		public int TenderId { get; set; }

		public virtual Tender Tender { get; set; }

		public int AwardId { get; set; }

		public virtual Award Award { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Товари, послуги та інші нематеріальні результати у цій угоді. 
		/// Зверніть увагу: Якщо список співпадає з визначенням переможця award, то його не потрібно повторювати.
		/// </summary>
		public virtual ICollection<Item> Items { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// </summary>
		public virtual ICollection<ContractSupplier> Suppliers { get; set; }

		/// <summary>
		/// OpenContracting Description: Усі документи та додатки пов’язані з договором, включно з будь-якими повідомленнями.
		/// </summary>
		public virtual ICollection<ContractDocument> Documents { get; set; }
	}
}
