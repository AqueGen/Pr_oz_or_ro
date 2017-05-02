using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface IModified
	{
		[JsonProperty("dateModified", DefaultValueHandling = DefaultValueHandling.Ignore)]
		DateTime DateModified { get; set; }
	}
}
