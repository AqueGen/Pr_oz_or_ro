using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IClassification
	{
		/// <summary>
		/// OpenContracting Description: Класифікація повинна бути взята з існуючої схеми або списку кодів. 
		/// Це поле використовується, щоб вказати схему/список кодів, з яких буде братись класифікація. 
		/// Для класифікацій лінійних елементів це значення повинно представляти відому Схему Класифікації Елементів, 
		/// де це можливо.
		/// </summary>
		string Scheme { get; set; }

		/// <summary>
		/// OpenContracting Description: Код класифікації взятий з вибраної схеми.
		/// </summary>
		string Id { get; set; }

		/// <summary>
		/// OpenContracting Description: Текстовий опис або назва коду.
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// OpenContracting Description: URI для ідентифікації коду. 
		/// Якщо індивідуальні URI не доступні для елементів у схемі ідентифікації це значення треба залишити пустим.
		/// </summary>
		string Uri { get; set; }
	}
}
