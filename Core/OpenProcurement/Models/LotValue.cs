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
	public class LotValue : BaseLotValue, ILotValue
	{
		/// <summary>
		/// StringId пов’язаного Lot.
		/// </summary>
		[JsonProperty("relatedLot")]
		public string RelatedLot { get; set; }
	}
}
