using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Models
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
    public abstract class BaseTender : BaseDraftTender, ITender
    {
        public BaseTender()
        {
        }

        public BaseTender(IDraftTender tender)
            : base(tender)
        {
        }

        public BaseTender(ITender tender)
            : base(tender)
        {
            Identifier = tender.Identifier;
            AuctionPeriod = tender.AuctionPeriod;
            AuctionUrl = tender.AuctionUrl;
            AwardPeriod = tender.AwardPeriod;
            Status = tender.Status;
            Owner = tender.Owner;
        }

        /// <summary>
        /// рядок, генерується автоматично, лише для читання
        /// Ідентифікатор закупівлі, щоб знайти закупівлю у “паперовій” документації
        /// OpenContracting Description: Ідентифікатор тендера TenderID повинен завжди співпадати з OCID.Його включають, щоб зробити структуру даних більш зручною.
        /// </summary>
        [JsonProperty("tenderID")]
        public string Identifier { get; set; }

        /// <summary>
        /// Лише для читання
        /// Період, коли проводиться аукціон.
        /// </summary>
        [JsonProperty("auctionPeriod")]
        public Period AuctionPeriod { get; set; }

        /// <summary>
        /// URL-адреса
        /// Веб-адреса для перегляду аукціону.
        /// </summary>
        [JsonProperty("auctionUrl")]
        public string AuctionUrl { get; set; }

        /// <summary>
        /// Лише для читання
        /// Період, коли відбувається визначення переможця.
        /// OpenContracting Description: Дата або період, коли очікується визначення переможця.
        /// </summary>
        [JsonProperty("awardPeriod")]
        public Period AwardPeriod { get; set; }

        /// <summary>
        /// Статус Закупівлі.
        /// </summary>
        [JsonProperty("status")]
        [StringLength(36), Truncate]
        public string Status { get; set; }

        /// <summary>
        /// Власник закупівлі.
        /// Тільки для читання.
        /// </summary>
        [JsonProperty("owner")]
        [StringLength(256), Truncate]
        public string Owner { get; set; }
    }
}