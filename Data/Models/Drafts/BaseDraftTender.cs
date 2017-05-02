using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models.Drafts
{
    /// <summary>
    /// Закупівля.
    ///
    /// Дати закупівлі повинні бути послідовними:
    /// Поточний час
    /// enquiryPeriod.startDate
    /// enquiryPeriod.endDate
    /// tenderPeriod.startDate
    /// tenderPeriod.endDate
    /// </summary>
    public abstract class BaseDraftTender : IDraftTender
    {
        public BaseDraftTender()
        {
        }

        public BaseDraftTender(IDraftTender tender)
        {
            Guid = tender.Guid;
            DateModified = tender.DateModified;
            Title = tender.Title;
            TitleEn = tender.TitleEn;
            Description = tender.Description;
            DescriptionEn = tender.DescriptionEn;
            Value = tender.Value;
            Guarantee = tender.Guarantee;
            MinimalStep = tender.MinimalStep;
            EnquiryPeriod = tender.EnquiryPeriod;
            TenderPeriod = tender.TenderPeriod;
            ProcurementMethod = tender.ProcurementMethod;
            ProcurementMethodRationale = tender.ProcurementMethodRationale;
            ProcurementMethodType = tender.ProcurementMethodType;
            Cause = tender.Cause;
            CauseDescription = tender.CauseDescription;
            AwardCriteria = tender.AwardCriteria;
        }

        public Guid Guid { get; set; }

        public DateTime DateModified { get; set; }

        /// <summary>
        /// Назва тендера, яка відображається у списках. Можна включити такі елементи:
        /// - код закупівлі(у системі управління організації-замовника)
        /// - періодичність закупівлі(щороку, щокварталу, і т.д.)
        /// - елемент, що закуповується
        /// - інша інформація
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_en")]
        public string TitleEn { get; set; }

        /// <summary>
        /// Детальний опис закупівлі
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_en")]
        public string DescriptionEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Повний доступний бюджет закупівлі. Пропозиції, що більші за value будуть відхилені.
        /// OpenContracting Description: Загальна кошторисна вартість закупівлі.
        /// </summary>
        [JsonRequired]
        [JsonProperty("value")]
        public Value Value { get; set; }

        /// <summary>
        /// Забезпечення тендерної пропозиції
        /// </summary>
        [NotMapped]
        [JsonProperty("guarantee")]
        public Guarantee Guarantee { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Мінімальний крок аукціону (редукціону). Правила валідації:
        /// Значення amount повинно бути меншим за Tender.value.amount
        /// Значення currency повинно бути або відсутнім, або співпадати з Tender.value.currency
        /// Значення valueAddedTaxIncluded повинно бути або відсутнім, або співпадати з Tender.value.valueAddedTaxIncluded
        /// </summary>
        // TOTO Taras 5: recheck if this field is required
        // Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
        //[JsonRequired]
        [JsonProperty("minimalStep")]
        public Value MinimalStep { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Період, коли дозволено задавати питання. Повинна бути вказана хоча б endDate дата.
        /// OpenContracting Description: Період, коли можна зробити уточнення та отримати відповіді на них.
        /// </summary>
        // TOTO Taras 5: recheck if this field is required
        // Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
        //[JsonRequired]
        [JsonProperty("enquiryPeriod")]
        public EnquiryPeriod EnquiryPeriod { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Період, коли подаються пропозиції. Повинна бути вказана хоча б endDate дата.
        /// OpenContracting Description: Період, коли закупівля відкрита для подачі пропозицій.
        /// Кінцева дата - це дата, коли перестають прийматись пропозиції.
        /// </summary>
        // TOTO Taras 5: recheck if this field is required
        // Поле позначено як обов'язкове в документації, проте в пісочниці іноді залишається порожнім.
        //[JsonRequired]
        [JsonProperty("tenderPeriod")]
        public Period TenderPeriod { get; set; }

        /// <summary>
        /// Метод закупівлі.
        /// </summary>
        [JsonProperty("procurementMethod")]
        [StringLength(32), Truncate]
        public string ProcurementMethod { get; set; }

        /// <summary>
        /// Обгрунтування використання такого методу закупівлі.
        /// </summary>
        [JsonProperty("procurementMethodRationale")]
        public string ProcurementMethodRationale { get; set; }

        /// <summary>
        /// Тип методу закупівлі.
        /// </summary>
        [JsonProperty("procurementMethodType")]
        [StringLength(32), Truncate]
        public string ProcurementMethodType { get; set; }

        /// <summary>
        /// рядок, обов’язковий для переговорної процедури / необов’язковий для переговорної процедури за нагальною потребою
        /// Підстава для використання “звичайної” переговорної процедури або переговорної процедури за нагальною потребою.
        /// Для більш детальної інформації дивіться статтю 35 Закону України “Про публічні закупівлі”.
        /// </summary>
        [JsonProperty("cause")]
        [StringLength(32), Truncate]
        public string Cause { get; set; }

        /// <summary>
        /// рядок, багатомовний, обов’язковий для переговорної процедури та переговорної процедури за нагальною потребою
        /// Обгрунтування використання “звичайної” переговорної процедури або переговорної процедури за нагальною потребою.
        /// </summary>
        [JsonProperty("causeDescription")]
        public string CauseDescription { get; set; }

        /// <summary>
        /// Критерій кваліфікації.
        /// Тільки для читання.
        /// </summary>
        [JsonProperty("awardCriteria")]
        [StringLength(32), Truncate]
        public string AwardCriteria { get; set; }
    }
}