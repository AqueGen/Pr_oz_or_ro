using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Exceptions
{
	class ErrorsResponce
	{
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("errors")]
		public Error[] Errors { get; set; }
	}
}
