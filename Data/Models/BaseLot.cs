using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Лот закупівлі
	/// </summary>
	public abstract class BaseLot : BaseDraftLot, ILot
	{
		public BaseLot()
		{
		}

		public BaseLot(IDraftLot lot)
			: base(lot)
		{
		}

		public BaseLot(ILot lot)
			: base(lot)
		{
			AuctionPeriod = lot.AuctionPeriod;
			AuctionUrl = lot.AuctionUrl;
			Status = lot.Status;
		}

		/// <summary>
		/// Період проведення аукціону.
		/// доступно лише для читання
		/// </summary>
		[JsonProperty("auctionPeriod")]
		public Period AuctionPeriod { get; set; }

		/// <summary>
		/// Веб-адреса для перегляду аукціону.
		/// </summary>
		[JsonProperty("auctionUrl")]
		public string AuctionUrl { get; set; }

		/// <summary>
		/// Статус лота закупівлі
		/// </summary>
		[JsonProperty("status")]
		[StringLength(32), Truncate]
		public string Status { get; set; }
	}
}
