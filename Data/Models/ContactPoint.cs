using Kapitalist.Data.Models.Attributes;
using Kapitalist.Data.Models.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kapitalist.Data.Models
{
    /// <summary>
    /// Контактна особа.
    /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
    /// </summary>
    public class ContactPointOptional : IContactPoint
    {
        public ContactPointOptional()
        {
        }

        public ContactPointOptional(IContactPoint contactPoint)
        {
            Name = contactPoint.Name;
            NameEn = contactPoint.NameEn;
            Email = contactPoint.Email;
            Telephone = contactPoint.Telephone;
            FaxNumber = contactPoint.FaxNumber;
            Url = contactPoint.Url;
            AvailableLanguage = contactPoint.AvailableLanguage;
        }

        /// <summary>
        /// Обов'язково!
        /// OpenContracting Description: Ім’я контактної особи, назва відділу чи контактного пункту для листування,
        /// що стосується цього процесу укладання договору.
        /// </summary>
        [StringLength(256), Truncate]
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("name_en")]
        public string NameEn { get; set; }

        /// <summary>
        /// OpenContracting Description: Адреса електронної пошти контактної особи/пункту.
        /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
        /// </summary>
        [StringLength(256), Truncate]
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// OpenContracting Description: Номер телефону контактної особи/пункту.
        /// Повинен включати міжнародний телефонний код.
        /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
        /// </summary>
        [StringLength(256), Truncate]
        [JsonProperty("telephone")]
        public string Telephone { get; set; }

        /// <summary>
        /// OpenContracting Description: Номер факсу контактної особи/пункту.
        /// Повинен включати міжнародний телефонний код.
        /// </summary>
        [StringLength(256), Truncate]
        [JsonProperty("faxNumber")]
        public string FaxNumber { get; set; }

        /// <summary>
        /// OpenContracting Description: Веб адреса контактної особи/пункту.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Визначає мови спілкування.
        /// Можливі значення:
        /// uk - українська мова
        /// en - англійська мова
        /// ru - російська мова
        /// </summary>
        [JsonProperty("availableLanguage")]
        [StringLength(32), Truncate]
        public string AvailableLanguage { get; set; }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Name)
                && string.IsNullOrEmpty(NameEn)
                && string.IsNullOrEmpty(Email)
                && string.IsNullOrEmpty(Telephone)
                && string.IsNullOrEmpty(FaxNumber)
                && string.IsNullOrEmpty(Url)
                && string.IsNullOrEmpty(AvailableLanguage);
        }
    }

    /// <summary>
    /// Контактна особа.
    /// Name обов'язково (задано через Fluent API)
    /// Повинне бути заповнене хоча б одне з полів: або email, або telephone.
    /// </summary>
    [ComplexType]
    public class ContactPoint : ContactPointOptional, IComplexType
    {
        public ContactPoint()
        {
        }

        public ContactPoint(IContactPoint contactPoint)
            : base(contactPoint)
        {
        }
    }
}