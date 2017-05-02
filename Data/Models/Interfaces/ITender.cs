using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface ITender : IGuid, IModified, ITitled, IAuctioned, IDraftTender
    {
        /// <summary>
        /// рядок, генерується автоматично, лише для читання
        /// Ідентифікатор закупівлі, щоб знайти закупівлю у “паперовій” документації
        /// OpenContracting Description: Ідентифікатор тендера TenderID повинен завжди співпадати з OCID.Його включають, щоб зробити структуру даних більш зручною.
        /// </summary>
        string Identifier { get; set; }

        /// <summary>
        /// Лише для читання
        /// Період, коли відбувається визначення переможця.
        /// OpenContracting Description: Дата або період, коли очікується визначення переможця.
        /// </summary>
        Period AwardPeriod { get; set; }

        /// <summary>
        /// Власник закупівлі.
        /// Тільки для читання.
        /// </summary>
        string Owner { get; set; }
    }
}