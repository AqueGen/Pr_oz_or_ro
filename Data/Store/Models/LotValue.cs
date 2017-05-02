using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Store.Models
{
	public class LotValue : BaseLotValue, ILotValue
	{
		public LotValue()
		{
		}

		public LotValue(ILotValue lotValue)
		{
			Value = lotValue.Value;
			Date = lotValue.Date;
			ParticipationUrl = lotValue.ParticipationUrl;
		}

		public int Id { get; set; }

		public int BidId { get; set; }

		public virtual Bid Bid { get; set; }

		public int? LotId { get; set; }

		public virtual Lot Lot { get; set; }
	}
}
