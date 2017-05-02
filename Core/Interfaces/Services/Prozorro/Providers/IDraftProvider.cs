using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Security;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface IDraftProvider
    {
        Task<Guid> AddDraftTender(DraftTenderDTO tenderDTO);
        Task AddDraftItem(Guid tenderGuid, ItemDTO itemDTO);
        Task<DraftTenderDTO> GetDraftTender(Guid guid);
        Task<IEnumerable<ItemDTO>> GetDraftItems(Guid tenderGuid);
        Task<ItemDTO> GetDraftItem(Guid tenderGuid, string itemId);
        Task EditDraftItem(Guid tenderGuid, ItemDTO itemDTO);
        Task<Guid?> PublishDraftTender(Guid tenderGuid);
        Task AddDraftFeatureValue(Guid tenderGuid, string featureId, FeatureValueDTO featureValueDTO);
        Task AddDraftLot(Guid tenderGuid, DraftLotDTO draftLotDTO);
        Task EditDraftTender(Guid tenderGuid, DraftTenderDTO draftTenderDTO);
        Task<DraftLotDTO> GetDraftLot(Guid tenderGuid, string lotId);
        Task EditDraftLot(Guid tenderGuid, DraftLotDTO draftLotDTO);
        Task<IEnumerable<DraftLotDTO>> GetDraftLots(Guid tenderGuid);
        Task<string> AddDraftFeature(Guid tenderGuid, FeatureDTO featureDTO);
        Task<IEnumerable<FeatureDTO>> GetDraftFeatures(Guid tenderGuid);
        Task<FeatureDTO> GetDraftFeature(Guid tenderGuid, string featureStringId);
        Task EditDraftFeature(Guid tenderGuid, FeatureDTO featureDTO);
        Task<FeatureValueDTO> GetDraftFeatureValue(Guid tenderGuid, int featureValueId);
        Task EditDraftFeatureValue(Guid tenderGuid, FeatureValueDTO featureValueDTO);
        Task<IEnumerable<DraftTenderDTO>> GetDraftTenders(int userOrganizationId);
        Task<IEnumerable<DraftPlanDTO>> GetDraftPlans(int userOrganizationId);
        //Task<List<ClassificationCPV>> GetClassificationsCPVs();
        //Task<List<ClassificationGSIN>> GetClassificationsGSINs();
        Task DeleteTender(Guid tenderGuid);
        Task DeleteItem(Guid tenderGuid, string itemId);
        Task DeleteLot(Guid tenderGuid, string lotId);
        Task DeleteFeature(Guid tenderGuid, string featureStringId);
        Task DeleteFeatureValue(Guid tenderGuid, string featureStringId, int featureValueId);
        Task AddDraftDocument(Guid tenderGuid, DraftTenderDocumentDTO documentDTO);
        Task<DraftTenderDocumentDTO> GetDraftDocument(Guid tenderGuid, string documentId);
        Task EditDraftDocument(Guid tenderGuid, DraftTenderDocumentDTO documentDTO);
        Task DeleteDraftDocument(Guid tenderGuid, string documentId);

        Task<IEnumerable<DraftTenderDocumentDTO>> GetDraftDocuments(Guid tenderGuid, RelatedTo relatedTo,
            string relatedId);

        Task<IEnumerable<FeatureValueDTO>> GetDraftFeatureValues(Guid tenderGuid, string featureStringId);
        Task<DraftLotDTO> GetDraftLot(Guid tenderGuid, int itemLotId);
        Task<IEnumerable<string>> GetTenderCPVClassifications(Guid tenderGuid);
        Task<IEnumerable<string>> GetTenderGSINClassifications(Guid tenderGuid);
        Task AddContact(Guid tenderGuid, int contactId);
        Task DeleteContact(Guid tenderGuid, int contactId);
    }
}