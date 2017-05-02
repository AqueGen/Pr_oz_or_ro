using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Договір
	/// </summary>
	public abstract class BaseContract : IContract
	{
		public BaseContract()
		{
		}

		public BaseContract(IContract contract)
		{
			StringId = contract.StringId;
			Identifier = contract.Identifier;
			ContractNumber = contract.ContractNumber;
			Title = contract.Title;
			Description = contract.Description;
			Value = contract.Value;
			Status = contract.Status;
			Period = contract.Period;
			DateSigned = contract.DateSigned;
		}

        /// <summary>
        /// uid, генерується автоматично
        /// OpenContracting Description: Ідентифікатор цього договору.
        /// </summary>
        [Required]
        public string StringId { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// </summary>
		[JsonProperty("contractID")]
		public string Identifier { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Award.id вказує на рішення, згідно якого видається договір.
		/// </summary>
		// TOTO Taras 5: recheck if this field is required
		// Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
		//[JsonRequired]
		[JsonProperty("contractNumber")]
		public string ContractNumber { get; set; }

		/// <summary>
		/// Обов'язково!
		/// OpenContracting Description: Назва договору
		/// </summary>
		// TOTO Taras 5: recheck if this field is required
		// Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
		//[JsonRequired]
		[JsonProperty("title")]
		public string Title { get; set; }

		/// <summary>
		/// OpenContracting Description: Опис договору
		/// </summary>
		[JsonProperty("description")]
		public string Description { get; set; }

		/// <summary>
		/// Генерується автоматично, лише для читання.
		/// OpenContracting Description: Загальна вартість договору.
		/// </summary>
		[JsonProperty("value")]
		public Value Value { get; set; }

		/// <summary>
		/// OpenContracting Description: Поточний статус договору.
		/// </summary>
		[JsonProperty("status")]
		[StringLength(32), Truncate]
		public string Status { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата початку та завершення договору.
		/// </summary>
		[JsonProperty("period")]
		public Period Period { get; set; }

		/// <summary>
		/// OpenContracting Description: Дата підписання договору. 
		/// Якщо було декілька підписань, то береться дата останнього підписання.
		/// </summary>
		[JsonProperty("dateSigned")]
		public DateTime? DateSigned { get; set; }
	}
}
