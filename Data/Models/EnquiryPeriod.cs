using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    [ComplexType]
    public class EnquiryPeriod : Period, IComplexType
    {
        public EnquiryPeriod()
        {
        }

        public EnquiryPeriod(Period period)
            : base(period)
        {
        }

        public EnquiryPeriod(EnquiryPeriod period)
            : base(period)
        {
            InvalidationDate = period.InvalidationDate;
            ClarificationsUntil = period.ClarificationsUntil;
        }

        /// <summary>
        /// Це дата останньої зміни умов, коли всі подані цінові пропозиції перейшли в стан invalid.
        /// Відповідно необхідні дії майданчика щодо активації чи переподачі пропозицій.
        /// </summary>
        [JsonProperty("invalidationDate")]
        public DateTime? InvalidationDate { get; set; }

        /// <summary>
        /// Час, до якого можна давати відповіді на запитання та вимоги, після якого блокується процедура.
        /// </summary>
        [JsonProperty("clarificationsUntil")]
        public DateTime? ClarificationsUntil { get; set; }

        public override bool IsEmpty()
        {
            return base.IsEmpty()
                && !InvalidationDate.HasValue
                && !ClarificationsUntil.HasValue;
        }
    }
}