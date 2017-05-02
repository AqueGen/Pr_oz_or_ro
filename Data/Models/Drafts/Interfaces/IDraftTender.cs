using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Models.Drafts.Interfaces
{
    public interface IDraftTender : IGuid, IModified, ITitled, IDraftAuctioned
    {
        string TitleEn { get; set; }

        string DescriptionEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Період, коли дозволено задавати питання. Повинна бути вказана хоча б endDate дата.
        /// OpenContracting Description: Період, коли можна зробити уточнення та отримати відповіді на них.
        /// </summary>
        EnquiryPeriod EnquiryPeriod { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Період, коли подаються пропозиції. Повинна бути вказана хоча б endDate дата.
        /// OpenContracting Description: Період, коли закупівля відкрита для подачі пропозицій.
        /// Кінцева дата - це дата, коли перестають прийматись пропозиції.
        /// </summary>
        Period TenderPeriod { get; set; }

        /// <summary>
        /// Метод закупівлі.
        /// </summary>
        [StringLength(32), Truncate]
        string ProcurementMethod { get; set; }

        /// <summary>
        /// Обгрунтування використання такого методу закупівлі.
        /// </summary>
        string ProcurementMethodRationale { get; set; }

        /// <summary>
        /// Тип методу закупівлі.
        /// </summary>
        string ProcurementMethodType { get; set; }

        /// <summary>
        /// рядок, обов’язковий для переговорної процедури / необов’язковий для переговорної процедури за нагальною потребою
        /// Підстава для використання “звичайної” переговорної процедури або переговорної процедури за нагальною потребою.
        /// Для більш детальної інформації дивіться статтю 35 Закону України “Про публічні закупівлі”.
        /// </summary>
        string Cause { get; set; }

        /// <summary>
        /// рядок, багатомовний, обов’язковий для переговорної процедури та переговорної процедури за нагальною потребою
        /// Обгрунтування використання “звичайної” переговорної процедури або переговорної процедури за нагальною потребою.
        /// </summary>
        string CauseDescription { get; set; }

        /// <summary>
        /// Критерій кваліфікації.
        /// Тільки для читання.
        /// </summary>
        string AwardCriteria { get; set; }
    }
}