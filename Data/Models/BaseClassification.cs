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
	/// Базова класифікація однієї схеми
	/// </summary>
	public abstract class BaseSingleClassification : IEquatable<BaseSingleClassification>
	{
		public BaseSingleClassification()
		{
		}

		public BaseSingleClassification(IClassification classification)
		{
			Id = classification.Id;
			Description = classification.Description;
		}

		/// <summary>
		/// OpenContracting Description: Код класифікації взятий з вибраної схеми.
		/// </summary>
		[JsonProperty("id")]
		[StringLength(32), Truncate]
		public virtual string Id { get; set; }

		/// <summary>
		/// OpenContracting Description: Текстовий опис або назва коду.
		/// </summary>
		[JsonProperty("description")]
		public virtual string Description { get; set; }

	    public bool Equals(BaseSingleClassification other)
	    {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;
	        return string.Equals(Id, other.Id) && string.Equals(Description, other.Description);
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((BaseSingleClassification) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            return ((Id != null ? Id.GetHashCode() : 0)*397) ^ (Description != null ? Description.GetHashCode() : 0);
	        }
	    }
	}

	/// <summary>
	/// Базова класифікація за багатьма схемами
	/// </summary>
	public abstract class BaseClassification : BaseSingleClassification, IClassification
	{
		public BaseClassification()
		{
		}

		public BaseClassification(IClassification classification)
			: base(classification)
		{
			Scheme = classification.Scheme;
			Uri = classification.Uri;
		}

		/// <summary>
		/// OpenContracting Description: Класифікація повинна бути взята з існуючої схеми або списку кодів. 
		/// Це поле використовується, щоб вказати схему/список кодів, з яких буде братись класифікація. 
		/// Для класифікацій лінійних елементів це значення повинно представляти відому Схему Класифікації Елементів, 
		/// де це можливо.
		/// </summary>
		[JsonProperty("scheme")]
		[StringLength(32), Truncate]
		public virtual string Scheme { get; set; }

		/// <summary>
		/// OpenContracting Description: URI для ідентифікації коду. 
		/// Якщо індивідуальні URI не доступні для елементів у схемі ідентифікації це значення треба залишити пустим.
		/// </summary>
		[JsonProperty("uri")]
		public string Uri { get; set; }
	}
}
