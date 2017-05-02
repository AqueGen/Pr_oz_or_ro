using Kapitalist.Data.Models.Drafts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Drafts
{
    public class BaseDraftContract : IDraftContract
    {
        public Guid Guid { get; set; }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Назва договору
        /// </summary>
        // TOTO Taras 5: recheck if this field is required
        // Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
        //[JsonRequired]
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// OpenContracting Description: Опис договору
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
