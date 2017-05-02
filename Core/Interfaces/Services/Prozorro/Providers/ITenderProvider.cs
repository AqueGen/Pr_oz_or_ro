using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using Kapitalist.Data.Models.Enums;
using PagedList;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface ITenderProvider
    {
        Task<IPagedList<TenderDTO>> GetTendersPage(int pageNumber, int pageSize = 10);

        Task<IPagedList<TenderDTO>> GetTendersPage(TenderQueryDTO tenderQuery, int pageNumber,
            int pageSize = 10);

        Task<TenderDTO> GetTender(Guid guid);

        Task<IEnumerable<LotDTO>> GetLots(int tenderId);

        Task<IEnumerable<LotDTO>> GetLots(Guid tenderGuid);

        Task<IEnumerable<ItemDTO>> GetItems(Guid tenderGuid);

        Task<ItemDTO> GetItem(Guid tenderGuid, string itemId);

        Task<IEnumerable<DocumentDTO>> GetTenderDocuments(int tenderId);

        Task EditTender(TenderDTO tenderDTO);

        Task AddItem(Guid tenderGuid, ItemDTO itemDTO);

        Task EditItem(Guid tenderGuid, ItemDTO itemDTO);

        Task<int> AddUserOrganization(OrganizationDTO userOrganizationDTO);

        Task DeleteUserOrganization(int id);

        Task AddQuestion(Guid tenderGuid, QuestionDTO questionDTO);

        Task<IEnumerable<QuestionDTO>> GetQuestions(int tenderId);

        Task<QuestionDTO> GetQuestion(Guid tenderGuid, string questionId);

        Task AddAnswer(Guid tenderGuid, QuestionDTO questioDTO);

        Task AddLot(Guid tenderGuid, LotDTO lotDTO);

        Task<LotDTO> GetLot(Guid tenderGuid, string lotId);

        Task EditLot(Guid tenderGuid, LotDTO lotDTO);

        Task<string> AddFeature(Guid tenderGuid, FeatureDTO featureDTO);

        Task<int> AddFeatureValue(Guid tenderGuid, string featureId, FeatureValueDTO featureValueDTO);

        Task<FeatureDTO> GetFeature(Guid tenderGuid, string featureStringId);

        Task EditFeature(Guid tenderGuid, FeatureDTO featureDTO);

        Task<FeatureValueDTO> GetFeatureValue(Guid tenderGuid, int featureValueId);

        Task EditValueFeature(FeatureValueDTO featureValueDTO);

        Task<IEnumerable<FeatureDTO>> GetFeatures(Guid tenderGuid);

        Task<OrganizationDTO> GetContactPoint(int userId);

        Task AddUserOrganizationIdentifiers(int organizationId,
            IdentifierDTO userOrganizationIdentifierDTO);

        bool IsTenderPresent(Guid tenderGuid);

        Task AddDocument(Guid tenderGuid, DocumentDTO documentDTO);

        Task<DocumentDTO> GetDocument(Guid tenderGuid, string documentId);

        Task EditDocument(Guid tenderGuid, DocumentDTO documentDTO);

        Task DeleteDocument(Guid tenderGuid, string documentId);

        Task<IEnumerable<DocumentDTO>> GetDocuments(Guid tenderGuid, RelatedTo relatedTo, string relatedId);

        Task<IEnumerable<FeatureValueDTO>> GetFeatureValues(Guid tenderGuid, string featureStringId);

        Task<IEnumerable<ComplaintDTO>> GetTendeComplaints(Guid tenderGuid);

        Task AddTenderComplaint(Guid tenderGuid, TenderComplaintDTO tenderComplaintDTO);

        Task<ComplaintDTO> GetTenderComplaint(Guid tenderGuid, string complaintId);

        Task EditTenderComplaint(TenderComplaintDTO tenderComplaintDTO);

        Task<LotDTO> GetLot(Guid tenderGuid, int itemLotId);

        Task AddComplaintAnswer(Guid tenderGuid, TenderComplaintDTO complaintDTO);

        Task<OrganizationDTO> GetTenderComplaintAuthor(Guid tenderGuid, string complaintId);

        Task CheckComplaintAuthor(Guid tenderGuid, string complaintId);

        Task CheckTenderAuthor(Guid tenderGuid);

        Task DeleteLot(Guid tenderGuid, string lotId);

        Task EditFeatureValue(Guid viewModelTenderGuid, FeatureValueDTO toDTO);

        Task DeleteFeatureValue(Guid tenderGuid, string featureId, int featureValueId);

        Task DeleteFeature(Guid tenderGuid, string featureId);

        Task DeleteItem(Guid tenderGuid, string itemId);

        Task<IEnumerable<string>> GetTenderCPVClassifications(Guid tenderGuid);
        Task<IEnumerable<string>> GetTenderGSINClassifications(Guid tenderGuid);

        Task<IEnumerable<TenderDTO>> GetTenders(int userOrganizationId);
    }
}