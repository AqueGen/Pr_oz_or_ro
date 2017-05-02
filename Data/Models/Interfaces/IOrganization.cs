namespace Kapitalist.Data.Models.Interfaces
{
    public interface IOrganization
    {
        /// <summary>
        /// OpenContracting Description: Назва організації.
        /// </summary>
        string Name { get; set; }

        string NameEn { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Адреса організації
        /// </summary>
        Address Address { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Контактна особа організації
        /// </summary>
        ContactPoint ContactPoint { get; set; }
    }
}