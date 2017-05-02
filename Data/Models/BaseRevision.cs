using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Зміни властивостей об’єктів Закупівлі
	/// </summary>
	public abstract class BaseRevision : IRevision
	{
		public BaseRevision()
		{
		}

		public BaseRevision(IRevision revision)
		{
			Date = revision.Date;
		}

		/// <summary>
		/// Дата, коли зміни були записані.
		/// </summary>
		[JsonProperty("date")]
		public DateTime Date { get; set; }
	}
}
