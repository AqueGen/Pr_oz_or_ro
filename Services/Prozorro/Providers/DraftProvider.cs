using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Core.OpenProcurement;
using Kapitalist.Core.OpenProcurement.Models.Root;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Consts;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Helpers;
using Kapitalist.Web.Security;
using Rest = Kapitalist.Core.OpenProcurement.Models;
using Kapitalist.Services.Prozorro.Mappers;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class DraftProvider : BaseProvider, IDraftProvider
    {
        public DraftProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task<Guid> AddDraftTender(DraftTenderDTO draftTenderDTO)
        {
            var tender = draftTenderDTO.ToDraft();
            tender.ProcuringEntityId = AccessManager.UserOrganizationId;
            Context.DraftTenders.Add(tender);
            await Context.SaveChangesAsync();
            return tender.Guid;
        }

        public async Task AddDraftItem(Guid tenderGuid, ItemDTO draftItemDTO)
        {
            var draftItem = draftItemDTO.ToDraft();
            var tender = await Context.DraftTenders
                .Include(m => m.Lots)
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var lot = tender.Lots.FirstOrDefault(m => m.StringId == draftItemDTO.LotStringId);
            draftItem.LotId = lot?.Id;
            tender.Items.Add(draftItem);
            await Context.SaveChangesAsync();
        }

        public async Task<DraftTenderDTO> GetDraftTender(Guid guid)
        {
            var tender = await Context.DraftTenders
                .Include(t => t.ProcuringEntity)
                .Include(t => t.ProcuringEntity.AllIdentifiers)
                .Include(t => t.Lots)
                .Include(t => t.Items)
                .Include(t => t.Items.Select(i => i.AdditionalClassifications))
                .Include(t => t.Features)
                .Include(t => t.Features.Select(f => f.Values))
                .Include(t => t.Documents)
                .Include(t => t.ContactPointRefs)
                .FirstOrDefaultAsync(t => t.Guid == guid);

            var tenderDTO = tender.ToDTO();
            return tenderDTO;
        }

        public async Task<IEnumerable<ItemDTO>> GetDraftItems(Guid tenderGuid)
        {
            var items = await Context.DraftItems.Where(i => i.Tender.Guid == tenderGuid).ToListAsync();
            return items.Select(i => i.ToDTO());
        }

        public async Task<ItemDTO> GetDraftItem(Guid tenderGuid, string itemId)
        {
            var item = await Context.DraftItems
                .Include(i => i.AdditionalClassifications)
                .FirstOrDefaultAsync(i => i.Tender.Guid == tenderGuid && i.StringId == itemId);
            return item.ToDTO();
        }

        public async Task EditDraftItem(Guid tenderGuid, ItemDTO itemDTO)
        {
            var lot = await Context.DraftLots
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == itemDTO.LotStringId);
            var savedItem = await Context.DraftItems
                .Include(i => i.AdditionalClassifications)
                .FirstOrDefaultAsync(i => i.Tender.Guid == tenderGuid && i.StringId == itemDTO.StringId);
            savedItem.LotId = lot?.Id;
            Context.Entry(savedItem).CurrentValues.SetValues(itemDTO.InitComplexProperties());

            Context.Replace(savedItem.AdditionalClassifications, itemDTO.AdditionalClassifications,
                (classification, dto) => { classification.Item = savedItem; });
            await Context.SaveChangesAsync();
        }

        public async Task<Guid?> PublishDraftTender(Guid tenderGuid)
        {
            // отримуємо всі звязані з тендером драфти з бази
            var draftTender = await Context.DraftTenders
                .Include(t => t.ProcuringEntity)
                .Include(t => t.ProcuringEntity.AllIdentifiers)
                .Include(t => t.ProcuringEntity.ContactPoints)
                .Include(t => t.ContactPointRefs)
                .Include(t => t.Lots)
                .Include(t => t.Items)
                .Include(t => t.Items.Select(i => i.AdditionalClassifications))
                .Include(t => t.Features)
                .Include(t => t.Features.Select(f => f.Values))
                .FirstOrDefaultAsync(t => t.Guid == tenderGuid);

            Protected<Rest.Tender> createdTenderInfo = null;
            // відправляємо запит в ЦБД
            using (var tendersService = new TendersService())
            {
                var newTender = draftTender.ToRest();
                switch (draftTender.ProcurementMethodType)
                {
                    case ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE:
                    case ProcurementMethodType.ABOVE_THRESHOLD_EU:
                        newTender.ProcuringEntity.ContactPoints = draftTender.ContactPointRefs.Select(c => new ContactPoint(c.ContactPoint));
                        break;
                    case ProcurementMethodType.REPORTING:
                    case ProcurementMethodType.NEGOTIATION:
                    case ProcurementMethodType.NEGOTIATION_QUICK:
                        newTender.Features = null;
                        newTender.MinimalStep = null;
                        newTender.Lots = null;
                       goto default;
                    default:
                        newTender.ProcuringEntity.ContactPoint.AvailableLanguage = null;
                        break;
                }
                createdTenderInfo = await tendersService.CreateTender(newTender);
            }

            // Якщо досі не вискочив вийняток - значить тендер успішно створений в ДБД
            // Відловимо всі наступні вийнятки - щоб клієнт зміг побачити, що тендер успішно створений
            try
            {
                CreatedTender createdTender = new CreatedTender
                {
                    Tender = Context.UpdateTender(null, createdTenderInfo.Data),
                    Token = createdTenderInfo.Token,
                    UserOrganizationId = draftTender.ProcuringEntityId
                };
                Context.CreatedTenders.Add(createdTender);
                Context.DraftTenders.Remove(draftTender);
                try
                {
                    await Context.SaveChangesAsync();
                }
                catch (UpdateException)
                {
                    // На випадок, ящо синхронізація пройшла швидше ніж додавання в базу при створенні
                    Trace.TraceError($"Draft tender {tenderGuid} must be deleted manually.");
                    using (StoreContext tempStore = new StoreContext())
                    {
                        Tender syncedTender =
                            tempStore.Tenders.FirstOrDefault(t => t.Guid == createdTenderInfo.Data.Guid);
                        if (syncedTender != null)
                        {
                            createdTender.Tender = syncedTender;
                            tempStore.CreatedTenders.Add(createdTender);
                            await tempStore.SaveChangesAsync();
                        }
                        else
                            throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    $"New tender {createdTenderInfo.Data?.Guid} was created by organization {draftTender.ProcuringEntityId} with token {createdTenderInfo.Token}, but cannot be saved to store. Correct it manually. {ex}");
            }
            return createdTenderInfo.Data?.Guid;
        }

        public async Task AddDraftFeatureValue(Guid tenderGuid, string featureId, FeatureValueDTO featureValueDTO)
        {
            var feature = await Context.DraftFeatures
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == featureId);
            feature.Values.Add(featureValueDTO.ToDraft());
            await Context.SaveChangesAsync();
        }

        public async Task AddDraftLot(Guid tenderGuid, DraftLotDTO draftLotDTO)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var draftLot = draftLotDTO.ToDraft();

            draftLot.Value.Currency = tender.Value.Currency;
            draftLot.Value.VATIncluded = tender.Value.VATIncluded;
            draftLot.MinimalStep.Currency = tender.MinimalStep.Currency;
            draftLot.MinimalStep.VATIncluded = tender.MinimalStep.VATIncluded;

            tender.Lots.Add(draftLot);
            await Context.SaveChangesAsync();
        }

        public async Task EditDraftTender(Guid tenderGuid, DraftTenderDTO draftTenderDTO)
        {
            var savedItem = await Context.DraftTenders
                .Include(m => m.ProcuringEntity)
                .Include(m => m.Lots)
                .FirstOrDefaultAsync(m => m.Guid == draftTenderDTO.Guid);
            draftTenderDTO.ProcuringEntityId = savedItem.ProcuringEntityId;

            if (draftTenderDTO.ProcurementMethodType == ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE
                || draftTenderDTO.ProcurementMethodType == ProcurementMethodType.ABOVE_THRESHOLD_EU
                || draftTenderDTO.ProcurementMethodType == ProcurementMethodType.ABOVE_THRESHOLD_UA
                || draftTenderDTO.ProcurementMethodType == ProcurementMethodType.BELOW_THRESHOLD
                )
            {
                draftTenderDTO.MinimalStep = savedItem.MinimalStep;
                draftTenderDTO.Guarantee = savedItem.Guarantee;

                draftTenderDTO.MinimalStep.Currency = draftTenderDTO.Value.Currency;
                draftTenderDTO.MinimalStep.VATIncluded = draftTenderDTO.Value.VATIncluded;
                draftTenderDTO.Guarantee.Currency = draftTenderDTO.Value.Currency;
            }

            foreach (var lot in savedItem.Lots)
            {
                lot.Value.Currency = draftTenderDTO.Value.Currency;
                lot.Value.VATIncluded = draftTenderDTO.Value.VATIncluded;

                lot.MinimalStep.Currency = draftTenderDTO.Value.Currency;
                lot.MinimalStep.VATIncluded = draftTenderDTO.Value.VATIncluded;

                lot.Guarantee.Currency = draftTenderDTO.Value.Currency;
            }

            Context.Entry(savedItem).CurrentValues.SetValues(draftTenderDTO.InitComplexProperties());
            await Context.SaveChangesAsync();
        }

        public async Task<DraftLotDTO> GetDraftLot(Guid tenderGuid, string lotId)
        {
            var lot = await Context.DraftLots
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == lotId);
            return lot.ToDTO();
        }

        public async Task EditDraftLot(Guid tenderGuid, DraftLotDTO draftLotDTO)
        {
            var savedItem = await Context.DraftLots
                .FirstOrDefaultAsync(i => i.Tender.Guid == tenderGuid && i.StringId == draftLotDTO.StringId);
            draftLotDTO.InitComplexProperties();

            draftLotDTO.Value.Currency = savedItem.Tender.Value.Currency;
            draftLotDTO.Value.VATIncluded = savedItem.Tender.Value.VATIncluded;
            draftLotDTO.MinimalStep.Currency = savedItem.Tender.MinimalStep.Currency;
            draftLotDTO.MinimalStep.VATIncluded = savedItem.Tender.MinimalStep.VATIncluded;

            Context.Entry(savedItem).CurrentValues.SetValues(draftLotDTO);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DraftLotDTO>> GetDraftLots(Guid tenderGuid)
        {
            var lots = await Context.DraftLots.Where(m => m.Tender.Guid == tenderGuid).ToListAsync();
            return lots.Select(l => l.ToDTO());
        }


        public async Task<string> AddDraftFeature(Guid tenderGuid, FeatureDTO draftFeatureDTO)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var draftFeature = draftFeatureDTO.ToDraft();
            tender.Features.Add(draftFeature);
            await Context.SaveChangesAsync();
            return draftFeature.StringId;
        }

        public async Task<IEnumerable<FeatureDTO>> GetDraftFeatures(Guid tenderGuid)
        {
            var features = await Context.DraftFeatures.Where(f => f.Tender.Guid == tenderGuid).ToListAsync();
            return features.Select(f => f.ToDTO());
        }

        public async Task<FeatureDTO> GetDraftFeature(Guid tenderGuid, string featureStringId)
        {
            var draftFeature = await Context.DraftFeatures
                .FirstOrDefaultAsync(f => f.Tender.Guid == tenderGuid && f.StringId == featureStringId);
            return draftFeature.ToDTO();
        }

        public async Task EditDraftFeature(Guid tenderGuid, FeatureDTO featureDTO)
        {
            var savedFeature = await Context.DraftFeatures
                .FirstOrDefaultAsync(i => i.Tender.Guid == tenderGuid && i.StringId == featureDTO.StringId);
            Context.Entry(savedFeature).CurrentValues.SetValues(featureDTO);
            await Context.SaveChangesAsync();
        }

        public async Task<FeatureValueDTO> GetDraftFeatureValue(Guid tenderGuid, int featureValueId)
        {
            var draftFeatureValue = await Context.DraftFeatureValues.FirstOrDefaultAsync(m => m.Id == featureValueId);
            return draftFeatureValue.ToDTO();
        }

        public async Task EditDraftFeatureValue(Guid tenderGuid, FeatureValueDTO featureValueDTO)
        {
            var savedFeature = await Context.DraftFeatureValues.FirstOrDefaultAsync(m => m.Id == featureValueDTO.Id);
            Context.Entry(savedFeature).CurrentValues.SetValues(featureValueDTO);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DraftTenderDTO>> GetDraftTenders(int userOrganizationId)
        {
            var draftTenders =
                await Context.DraftTenders.Where(m => m.ProcuringEntityId == userOrganizationId).ToListAsync();
            return draftTenders.Select(t => t.ToDTO());
        }

        public async Task<IEnumerable<DraftPlanDTO>> GetDraftPlans(int userOrganizationId)
        {
            var draftPlans =
                await Context.DraftPlans.Where(m => m.ProcuringEntityId == userOrganizationId).ToListAsync();
            return draftPlans.Select(p => p.ToDTO());
        }

        //public Task<List<ClassificationDTO>> GetClassificationsCPVs()
        //{
        //    return Context.ClassificationsCPV.ToListAsync();
        //}

        //public Task<List<ClassificationDTO>> GetClassificationsGSINs()
        //{
        //    return Context.ClassificationsGSIN.ToListAsync();
        //}

        public async Task DeleteTender(Guid tenderGuid)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            Context.DraftTenders.Remove(tender);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteItem(Guid tenderGuid, string itemId)
        {
            var item =
                await Context.DraftItems.FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == itemId);
            Context.DraftItems.Remove(item);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteLot(Guid tenderGuid, string lotId)
        {
            var lot =
                await Context.DraftLots.FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == lotId);
            Context.DraftLots.Remove(lot);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteFeature(Guid tenderGuid, string featureStringId)
        {
            var feature = await Context.DraftFeatures
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == featureStringId);
            Context.DraftFeatures.Remove(feature);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteFeatureValue(Guid tenderGuid, string featureStringId, int featureValueId)
        {
            var featureValue = await Context.DraftFeatureValues
                .FirstOrDefaultAsync(v => v.Id == featureValueId);
            Context.DraftFeatureValues.Remove(featureValue);
            await Context.SaveChangesAsync();
        }

        public async Task AddDraftDocument(Guid tenderGuid, DraftTenderDocumentDTO documentDTO)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            tender.Documents.Add(documentDTO.ToDraft());
            await Context.SaveChangesAsync();
        }

        public async Task<DraftTenderDocumentDTO> GetDraftDocument(Guid tenderGuid, string documentId)
        {
            var document = await Context.DraftTenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentId);
            return document.ToDTO();
        }

        public async Task EditDraftDocument(Guid tenderGuid, DraftTenderDocumentDTO documentDTO)
        {
            var document = await Context.DraftTenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentDTO.StringId);
            Context.Entry(document).CurrentValues.SetValues(documentDTO);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteDraftDocument(Guid tenderGuid, string documentId)
        {
            var document = await Context.DraftTenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentId);
            Context.DraftTenderDocuments.Remove(document);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DraftTenderDocumentDTO>> GetDraftDocuments(Guid tenderGuid, RelatedTo relatedTo,
            string relatedId)
        {
            var draftTenderDocuments = await Context.DraftTenderDocuments
                .Where(m => m.Tender.Guid == tenderGuid && m.DocumentOf == relatedTo && m.RelatedId == relatedId)
                .ToListAsync();
            return draftTenderDocuments.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<FeatureValueDTO>> GetDraftFeatureValues(Guid tenderGuid,
            string featureStringId)
        {
            var featureValues =
                await
                    Context.DraftFeatureValues.Where(
                        m => m.Feature.Tender.Guid == tenderGuid && m.Feature.StringId == featureStringId).ToListAsync();
            return featureValues.Select(m => m.ToDTO());
        }

        public async Task<DraftLotDTO> GetDraftLot(Guid tenderGuid, int itemLotId)
        {
            var lot = await Context.DraftLots
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.Id == itemLotId);
            return lot.ToDTO();
        }

        public async Task<IEnumerable<string>> GetTenderCPVClassifications(Guid tenderGuid)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var itemKeys = tender.Items
                .Select(m => m.Classification.Id)
                .GroupBy(m => m)
                .Select(m => m.Key);

            return itemKeys;
        }

        public Task<IEnumerable<string>> GetTenderGSINClassifications(Guid tenderGuid)
        {
            return null;
        }

        public async Task AddContact(Guid tenderGuid, int contactId)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var draftContactPoint = new DraftTenderContactPoint
            {
                Tender = tender,
                ContactPointId = contactId
            };
            tender.ContactPointRefs.Add(draftContactPoint);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteContact(Guid tenderGuid, int contactId)
        {
            var tender = await Context.DraftTenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var contactPointRefs = tender.ContactPointRefs.FirstOrDefault(m => m.ContactPointId == contactId);
            tender.ContactPointRefs.Remove(contactPointRefs);
            await Context.SaveChangesAsync();
        }
    }
}