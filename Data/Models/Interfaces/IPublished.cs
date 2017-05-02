using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface IPublished
    {
        [JsonProperty("datePublished", DefaultValueHandling = DefaultValueHandling.Ignore)]
        DateTime DatePublished { get; set; }
    }
}
