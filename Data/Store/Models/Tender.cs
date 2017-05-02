using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class Tender : BaseTender, ITender
    {
        public Tender()
        {
        }

        public Tender(ITender tender)
            : base(tender)
        {
        }

        public int Id { get; set; }

        // Concurency resolving timestamp
        // EF includes a property in the where clause, during the update operation, if the property is marked with the Timestamp attribute.
        // http://www.entityframeworktutorial.net/EntityFramework5/handle-concurrency-in-entity-framework.aspx
        [Timestamp]
        public byte[] RowVersion { get; set; }

        internal GuaranteeOptional GuaranteeOptional
        {
            get { return Guarantee; }
            set { Guarantee = value; }
        }

        public DateTime DateSynced { get; set; }

        public virtual ProcuringEntity ProcuringEntity { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Список, який містить елемент, що закуповується.
        /// OpenContracting Description: Товари та послуги, що будуть закуплені, поділені на спискові елементи, де це можливо.
        /// Елементи не повинні дублюватись, замість цього вкажіть кількість 2.
        /// </summary>
        public virtual ICollection<Item> Items { get; set; }

        /// <summary>
        /// Властивості закупівлі.
        /// </summary>
        public virtual ICollection<Feature> Features { get; set; }

        /// <summary>
        /// OpenContracting Description: Всі документи та додатки пов’язані із закупівлею.
        /// </summary>
        public virtual ICollection<TenderDocument> Documents { get; set; }

        /// <summary>
        /// Питання до замовника procuringEntity і відповіді на них.
        /// </summary>
        public virtual ICollection<Question> Questions { get; set; }

        /// <summary>
        /// Скарги на умови закупівлі та їх вирішення.
        /// </summary>
        public virtual ICollection<TenderComplaint> Complaints { get; set; }

        /// <summary>
        /// Список усіх пропозицій зроблених під час закупівлі разом із інформацією про учасників закупівлі,
        /// їхні пропозиції та інша кваліфікаційна документація.
        /// OpenContracting Description: Список усіх компаній, які подали заявки для участі у закупівлі.
        /// </summary>
        public virtual ICollection<Bid> Bids { get; set; }

        /// <summary>
        /// Усі кваліфікації (дискваліфікації та переможці).
        /// </summary>
        public virtual ICollection<Award> Awards { get; set; }

        /// <summary>
        /// Список об’єктів Contract
        /// </summary>
        public virtual ICollection<Contract> Contracts { get; set; }

        /// <summary>
        /// Містить всі лоти закупівлі.
        /// </summary>
        public virtual ICollection<Lot> Lots { get; set; }

        /// <summary>
        /// Містить 1 об’єкт зі статусом active на випадок, якщо закупівлю буде відмінено.
        /// Об’єкт Cancellation описує причину скасування закупівлі та надає відповідні документи, якщо такі є.
        /// </summary>
        public virtual ICollection<Cancellation> Cancellations { get; set; }

        /// <summary>
        /// Генерується автоматично, лише для читання
        /// Зміни властивостей об’єктів Закупівлі
        /// </summary>
        public virtual ICollection<Revision> Revisions { get; set; }
    }
}