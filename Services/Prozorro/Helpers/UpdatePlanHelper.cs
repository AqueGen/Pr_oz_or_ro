using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using System;

namespace Kapitalist.Services.Prozorro.Helpers
{
    /// <summary>
    ///
    /// </summary>
    internal static class UpdatePlanHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="store"></param>
        /// <param name="saved"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        public static Plan UpdatePlan(this StoreContext store, Plan saved, Core.OpenProcurement.Models.Plan rest)
        {
            Plan plan = null;
            // Оновлюємо закупівлю, разом зі всіма залежними об'єктами
            store.ReplaceSingle(saved, rest, (savedPlan, restPlan) =>
            {
                plan = savedPlan;
                savedPlan.DateSynced = DateTime.UtcNow;
                store.ReplaceSingle(savedPlan.ProcuringEntity, restPlan.ProcuringEntity, (savedOrg, restOrg) =>
                {
                    savedOrg.Plan = savedPlan;
                    store.ReplaceSingle(savedOrg.Identifier, restOrg.Identifier, (savedIdentifier, restIdentifier) =>
                    {
                        savedIdentifier.Main = true;
                        savedIdentifier.Organization = savedOrg;
                    });
                    store.Replace(savedOrg.AdditionalIdentifiers, restOrg.AdditionalIdentifiers, (savedIdentifier, restIdentifier) =>
                    {
                        savedIdentifier.Organization = savedOrg;
                    });
                });
                savedPlan.Classification = new ClassificationCPVOptional(restPlan.Classification);
                store.Replace(savedPlan.AdditionalClassifications, restPlan.AdditionalClassifications, (savedClassification, restClassification) =>
                {
                    savedClassification.Plan = savedPlan;
                });
                store.Update(savedPlan.Items, restPlan.Items, (savedItem, restItem) =>
                {
                    savedItem.Plan = savedPlan;
                    savedItem.Classification = new ClassificationCPVOptional(restItem.Classification);
                    store.Replace(savedItem.AdditionalClassifications, restItem.AdditionalClassifications, (savedClassification, restClassification) =>
                    {
                        savedClassification.Item = savedItem;
                    });
                });
            });
            return plan;
        }
    }
}