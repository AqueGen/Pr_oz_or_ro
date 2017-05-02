using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models
{
	/// <summary>
	/// Зміни властивостей об’єктів Закупівлі
	/// </summary>
	public class Revision : BaseRevision, IRevision
	{
		/// <summary>
		/// Список об’єктів Change
		/// </summary>
		[JsonProperty("changes")]
		public Change[] Changes { get; set; }
	}
}
