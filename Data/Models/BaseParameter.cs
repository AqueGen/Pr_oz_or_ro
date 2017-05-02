using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Критерій
	/// </summary>
	public abstract class BaseParameter : IParameter
	{
		public BaseParameter()
		{
		}

		public BaseParameter(IParameter parameter)
		{
			Code = parameter.Code;
			Value = parameter.Value;
		}

		/// <summary>
		/// Обов'язково!
		/// Код критерію.
		/// </summary>
		[Required]
		[StringLength(128), Truncate]
		[JsonRequired]
		[JsonProperty("code")]
		public string Code { get; set; }

		/// <summary>
		/// Обов'язково!
		/// Значення критерію.
		/// </summary>
		[JsonRequired]
		[JsonProperty("value")]
		public float Value { get; set; }
	}
}
