namespace Kapitalist.Data.Models.Interfaces
{
    public interface IContactPoint
    {
        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Ім’я контактної особи, назва відділу чи контактного пункту для листування,
        /// що стосується цього процесу укладання договору.
        /// </summary>
        string Name { get; set; }

        string NameEn { get; set; }

        /// <summary>
        /// OpenContracting Description: Адреса електронної пошти контактної особи/пункту.
        /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// OpenContracting Description: Номер телефону контактної особи/пункту.
        /// Повинен включати міжнародний телефонний код.
        /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
        /// </summary>
        string Telephone { get; set; }

        /// <summary>
        /// OpenContracting Description: Номер факсу контактної особи/пункту.
        /// Повинен включати міжнародний телефонний код.
        /// </summary>
        string FaxNumber { get; set; }

        /// <summary>
        /// OpenContracting Description: Веб адреса контактної особи/пункту.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Визначає мови спілкування.
        /// Можливі значення:
        /// uk - українська мова
        /// en - англійська мова
        /// ru - російська мова
        /// </summary>
        string AvailableLanguage { get; set; }
    }
}