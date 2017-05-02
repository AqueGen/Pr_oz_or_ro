using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Core.OpenProcurement.Models
{
    /// <summary>
    /// Запитання
    /// </summary>
    public class Question : BaseQuestion
    {
        public Question()
        {
        }

        public Question(IQuestion question)
            : base(question)
        {
        }

        /// <summary>
        /// Обов'язково!
        /// Хто задає питання (contactPoint - людина, identification - організація, яку ця людина представляє).
        /// </summary>
        // TOTO Taras 5: recheck if this field is required
        // Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
        //[JsonRequired]
        [JsonProperty("author")]
        public Organization Author { get; set; }
    }
}
