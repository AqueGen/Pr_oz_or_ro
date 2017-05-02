using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Неціновий критерій
    /// </summary>
    public class Feature : BaseFeature
    {
        public Feature()
        {
        }

        public Feature(IFeature feature)
            : base(feature)
        {
        }

        /// <summary>
        /// Список значень критерію
        /// </summary>
        [JsonProperty("enum")]
        public FeatureValue[] Values { get; set; }
    }
}
