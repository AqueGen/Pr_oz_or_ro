using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kapitalist.Services.Prozorro.Helpers
{
    /// <summary>
    ///
    /// </summary>
    internal static class TenderHelper
    {
        public static async Task<Tender> GetTenderAsync(this StoreContext store, Guid guid, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await store.Tenders
                .Include(t => t.ProcuringEntity)
                .Include(t => t.ProcuringEntity.AllIdentifiers)
                .Include(t => t.ProcuringEntity.ContactPoints)
                .Include(t => t.Lots)
                .Include(t => t.Items)
                .Include(t => t.Items.Select(i => i.AdditionalClassifications))
                .Include(t => t.Features)
                .Include(t => t.Features.Select(f => f.Values))
                .Include(t => t.Documents)
                .Include(t => t.Questions)
                .Include(t => t.Questions.Select(q => q.Author))
                .Include(t => t.Questions.Select(q => q.Author.AllIdentifiers))
                .Include(t => t.Complaints)
                .Include(t => t.Complaints.Select(c => c.Author))
                .Include(t => t.Complaints.Select(c => c.Author.AllIdentifiers))
                .Include(t => t.Complaints.Select(c => c.Documents))
                .Include(t => t.Bids)
                .Include(t => t.Bids.Select(b => b.Tenderers))
                .Include(t => t.Bids.Select(b => b.Tenderers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Bids.Select(b => b.Documents))
                .Include(t => t.Bids.Select(b => b.Parameters))
                .Include(t => t.Bids.Select(b => b.LotValues))
                .Include(t => t.Awards)
                .Include(t => t.Awards.Select(a => a.Suppliers))
                .Include(t => t.Awards.Select(a => a.Suppliers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Awards.Select(a => a.Documents))
                .Include(t => t.Awards.Select(a => a.Items))
                .Include(t => t.Awards.Select(a => a.Complaints))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Author)))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Author.AllIdentifiers)))
                .Include(t => t.Awards.Select(a => a.Complaints.Select(c => c.Documents)))
                .Include(t => t.Contracts)
                .Include(t => t.Contracts.Select(c => c.Suppliers))
                .Include(t => t.Contracts.Select(c => c.Suppliers.Select(o => o.AllIdentifiers)))
                .Include(t => t.Contracts.Select(c => c.Documents))
                .Include(t => t.Contracts.Select(c => c.Items))
                .Include(t => t.Cancellations)
                .Include(t => t.Cancellations.Select(c => c.Documents))
                .Include(t => t.Revisions)
                .Include(t => t.Revisions.Select(r => r.Changes))
                .FirstOrDefaultAsync(t => t.Guid == guid, cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="store"></param>
        /// <param name="saved"></param>
        /// <param name="rest"></param>
        /// <returns></returns>
        public static Tender UpdateTender(this StoreContext store, Tender saved, Core.OpenProcurement.Models.Tender rest)
        {
            Tender tender = null;
            // Оновлюємо закупівлю, разом зі всіма залежними об'єктами
            store.ReplaceSingle(saved, rest, (savedTender, restTender) =>
            {
                tender = savedTender;
                savedTender.DateSynced = DateTime.UtcNow;
                store.ReplaceSingle(savedTender.ProcuringEntity, restTender.ProcuringEntity, (savedOrg, restOrg) =>
                {
                    savedOrg.Tender = savedTender;
                    store.ReplaceSingle(savedOrg.Identifier, restOrg.Identifier, (savedIdentifier, restIdentifier) =>
                    {
                        savedIdentifier.Main = true;
                        savedIdentifier.Organization = savedOrg;
                    });
                    store.Replace(savedOrg.AdditionalIdentifiers, restOrg.AdditionalIdentifiers, (savedIdentifier, restIdentifier) =>
                    {
                        savedIdentifier.Organization = savedOrg;
                    });
                    int contactsOrder = 0;
                    store.Replace(savedOrg.ContactPoints?.OrderBy(c => c.SortingOrder), restOrg.ContactPoints, (savedContactPoint, restContactPoint) =>
                    {
                        savedContactPoint.Organization = savedOrg;
                        savedContactPoint.SortingOrder = contactsOrder++;
                    });
                });
                store.Update(savedTender.Lots, restTender.Lots, (savedLot, restLot) =>
                {
                    savedLot.Tender = savedTender;
                });
                store.Update(savedTender.Items, restTender.Items, (savedItem, restItem) =>
                {
                    savedItem.Tender = savedTender;
                    savedItem.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restItem.RelatedLot);
                    savedItem.Classification = new ClassificationCPVOptional(restItem.Classification);
                    store.Replace(savedItem.AdditionalClassifications, restItem.AdditionalClassifications, (savedClassification, restClassification) =>
                    {
                        savedClassification.Item = savedItem;
                    });
                });
                store.Update(savedTender.Features, restTender.Features, (savedFeature, restFeature) =>
                {
                    savedFeature.Tender = savedTender;
                    store.Replace(savedFeature.Values, restFeature.Values, (savedValue, restValue) =>
                    {
                        savedValue.Feature = savedFeature;
                    });
                });
                store.UpdateDocuments(savedTender.Documents, restTender.Documents, (savedDoc, restDoc) =>
                {
                    savedDoc.Tender = savedTender;
                });
                store.Update(savedTender.Questions, restTender.Questions, (savedQuestion, restQuestion) =>
                {
                    savedQuestion.Tender = savedTender;
                    store.ReplaceSingle(savedQuestion.Author, restQuestion.Author, (savedOrg, restOrg) =>
                    {
                        savedOrg.Question = savedQuestion;
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
                });
                store.Update(savedTender.Complaints, restTender.Complaints, (savedComplaint, restComplaint) =>
                {
                    savedComplaint.Tender = savedTender;
                    savedComplaint.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restComplaint.RelatedLot);
                    store.ReplaceSingle(savedComplaint.Author, restComplaint.Author, (savedOrg, restOrg) =>
                    {
                        savedOrg.Complaint = savedComplaint;
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
                    store.UpdateDocuments(savedComplaint.Documents, restComplaint.Documents, (savedDoc, restDoc) =>
                    {
                        savedDoc.Complaint = savedComplaint;
                    });
                });
                store.Update(savedTender.Bids, restTender.Bids, (savedBid, restBid) =>
                {
                    savedBid.Tender = savedTender;
                    store.Replace(savedBid.Tenderers, restBid.Tenderers, (savedOrg, restOrg) =>
                    {
                        savedOrg.Bid = savedBid;
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
                    store.UpdateDocuments(savedBid.Documents, restBid.Documents, (savedDoc, restDoc) =>
                    {
                        savedDoc.Bid = savedBid;
                    });
                    store.Replace(savedBid.Parameters, restBid.Parameters, (savedParam, restParam) =>
                    {
                        savedParam.Bid = savedBid;
                    });
                    store.Replace(savedBid.LotValues, restBid.LotValues, (savedValue, restValue) =>
                    {
                        savedValue.Bid = savedBid;
                        savedValue.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restValue.RelatedLot);
                    });
                });
                store.Update(savedTender.Awards, restTender.Awards, (savedAward, restAward) =>
                {
                    savedAward.Tender = savedTender;
                    savedAward.Bid = savedTender.Bids?.FirstOrDefault(b => b.StringId == restAward.BidStringId);
                    savedAward.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restAward.LotStringId);
                    store.Replace(savedAward.Suppliers, restAward.Suppliers, (savedOrg, restOrg) =>
                    {
                        savedOrg.Award = savedAward;
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
                    store.UpdateDocuments(savedAward.Documents, restAward.Documents, (savedDoc, restDoc) =>
                    {
                        savedDoc.Award = savedAward;
                    });
                    store.Update(savedAward.Complaints, restAward.Complaints, (savedComplaint, restComplaint) =>
                    {
                        savedComplaint.Award = savedAward;
                        savedComplaint.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restComplaint.RelatedLot);
                        store.ReplaceSingle(savedComplaint.Author, restComplaint.Author, (savedOrg, restOrg) =>
                        {
                            savedOrg.Complaint = savedComplaint;
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
                        store.UpdateDocuments(savedComplaint.Documents, restComplaint.Documents, (savedDoc, restDoc) =>
                        {
                            savedDoc.Complaint = savedComplaint;
                        });
                    });
                    savedAward.Items = savedTender.Items.UpdateLinks(savedAward.Items, restAward.Items);
                });
                store.Update(savedTender.Contracts, restTender.Contracts, (savedContract, restContract) =>
                {
                    savedContract.Tender = savedTender;
                    savedContract.Award = savedTender.Awards.First(a => a.StringId == restContract.AwardStringId);
                    store.Replace(savedContract.Suppliers, restContract.Suppliers, (savedOrg, restOrg) =>
                    {
                        savedOrg.Contract = savedContract;
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
                    store.UpdateDocuments(savedContract.Documents, restContract.Documents, (savedDoc, restDoc) =>
                    {
                        savedDoc.Contract = savedContract;
                    });
                    savedContract.Items = savedTender.Items.UpdateLinks(savedContract.Items, restContract.Items);
                });
                store.Update(savedTender.Cancellations, restTender.Cancellations, (savedCancellation, restCancellation) =>
                {
                    savedCancellation.Tender = savedTender;
                    savedCancellation.Lot = savedTender.Lots?.FirstOrDefault(l => l.StringId == restCancellation.RelatedLot);
                    store.UpdateDocuments(savedCancellation.Documents, restCancellation.Documents, (savedDoc, restDoc) =>
                    {
                        savedDoc.Cancellation = savedCancellation;
                    });
                });
                store.Replace(savedTender.Revisions, restTender.Revisions, (savedRevision, restRevision) =>
                {
                    savedRevision.Tender = savedTender;
                    store.Replace(savedRevision.Changes, restRevision.Changes, (savedChange, restChange) =>
                    {
                        savedChange.Revision = savedRevision;
                    });
                });
            });
            return tender;
        }
    }
}