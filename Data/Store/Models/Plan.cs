using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kapitalist.Data.Store.Models
{
    public class Plan : BasePlan, IPlan
    {
        public Plan()
        {
        }

        public Plan(IPlan plan)
            : base(plan)
        {
            // EF ComplexType objects cannot be null
            // MinimalStep is required but sometimes prozoro returns null
            if (Classification == null)
                Classification = new ClassificationCPVOptional();
            if (Budget == null)
                Budget = new Budget();
            if (Budget.Project == null)
                Budget.Project = new Project();
            if (Tender == null)
                Tender = new PlanTender();
            if (Tender.TenderPeriod == null)
                Tender.TenderPeriod = new Period();
        }

        public int Id { get; set; }

        // Concurency resolving timestamp
        // EF includes a property in the where clause, during the update operation, if the property is marked with the Timestamp attribute.
        // http://www.entityframeworktutorial.net/EntityFramework5/handle-concurrency-in-entity-framework.aspx
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public ClassificationCPVOptional Classification { get; set; }

        public virtual ICollection<PlanClassification> AdditionalClassifications { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Список, який містить елемент, що закуповується.
        /// OpenContracting Description: Товари та послуги, що будуть закуплені, поділені на спискові елементи, де це можливо.
        /// Елементи не повинні дублюватись, замість цього вкажіть кількість 2.
        /// </summary>
        public virtual ICollection<PlanItem> Items { get; set; }

        /// <summary>
        /// Обов'язково!
        /// Замовник (організація, що проводить закупівлю).
        /// OpenContracting Description: Об’єкт, що управляє закупівлею. Він не обов’язково є покупцем, який платить / використовує закуплені елементи.
        /// </summary>
        public virtual PlanProcuringEntity ProcuringEntity { get; set; }

        public DateTime DateSynced { get; set; }
    }
}