using Kapitalist.Data.Models.Drafts;
using Kapitalist.Data.Models.Drafts.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace Kapitalist.Data.Store.Models
{
    public class DraftTender : BaseDraftTender, IDraftTender
    {
        public DraftTender()
        {
        }

        public DraftTender(IDraftTender tender)
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

        public int ProcuringEntityId { get; set; }

        public virtual UserOrganization ProcuringEntity { get; set; }

        public virtual ICollection<DraftTenderContactPoint> ContactPointRefs { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Список, який містить елемент, що закуповується.
        /// OpenContracting Description: Товари та послуги, що будуть закуплені, поділені на спискові елементи, де це можливо.
        /// Елементи не повинні дублюватись, замість цього вкажіть кількість 2.
        /// </summary>
        public virtual ICollection<DraftItem> Items { get; set; }

        /// <summary>
        /// Властивості закупівлі.
        /// </summary>
        public virtual ICollection<DraftFeature> Features { get; set; }

        /// <summary>
        /// OpenContracting Description: Всі документи та додатки пов’язані із закупівлею.
        /// </summary>
        public virtual ICollection<DraftTenderDocument> Documents { get; set; }

        ///// <summary>
        ///// Питання до замовника procuringEntity і відповіді на них.
        ///// </summary>
        //public virtual ICollection<DraftQuestion> Questions { get; set; }

        /// <summary>
        /// Містить всі лоти закупівлі.
        /// </summary>
        public virtual ICollection<DraftLot> Lots { get; set; }

        public void OnLotChange(DraftLot lot, EntityState state)
        {
            if (state == EntityState.Deleted)
            {
                this.Value.Amount -= lot.Value.Amount;
                this.MinimalStep.Amount -= lot.MinimalStep.Amount;
                this.Guarantee.Amount -= lot.Guarantee.Amount;
            }
            else if (state == EntityState.Added || state == EntityState.Modified)
            {
                var totals = Lots.GroupBy(l => 1)
                .Select(l => new
                {
                    Amount = l.Sum(i => i.Value.Amount),
                    MinimalStep = l.Sum(i => i.MinimalStep.Amount),
                    Guarantee = l.Sum(i => i.Guarantee.Amount)
                }).First();

                this.Value.Amount = totals.Amount;
                this.MinimalStep.Amount = totals.MinimalStep;
                this.Guarantee.Amount = totals.MinimalStep;
            }
        }
    }
}