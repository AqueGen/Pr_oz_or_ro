using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IAuctioned : IDraftAuctioned
	{
		/// <summary>
		/// Лише для читання
		/// Період, коли проводиться аукціон.
		/// </summary>
		Period AuctionPeriod { get; set; }

		/// <summary>
		/// URL-адреса
		/// Веб-адреса для перегляду аукціону.
		/// </summary>
		string AuctionUrl { get; set; }

		/// <summary>
		/// Статус закупівлі чи лоту.
		/// </summary>
		string Status { get; set; }
	}
}
