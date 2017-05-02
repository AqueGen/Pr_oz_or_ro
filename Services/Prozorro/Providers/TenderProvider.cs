using Kapitalist.Core.OpenProcurement;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Services.Prozorro.Exceptions;
using Kapitalist.Services.Prozorro.Helpers;
using Kapitalist.Services.Prozorro.Mappers;
using Kapitalist.Services.Prozorro.Providers.Models;
using Kapitalist.Web.Security;
using LinqKit;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Extensions;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class TenderProvider : BaseProvider, ITenderProvider
    {
        public TenderProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task<IPagedList<TenderDTO>> GetTendersPage(int pageNumber, int pageSize = 10)
        {
            var tenders = Context.Tenders.AsQueryable();
            // Сортування обов'язкове.
            // Пейджинг не працює на невідсортованому списку.
            tenders = SortTenders(tenders);
            return await GetTendersPage(tenders, pageNumber, pageSize);
        }

        public async Task<IPagedList<TenderDTO>> GetTendersPage(TenderQueryDTO tenderQuery, int pageNumber,
            int pageSize = 10)
        {
            // AsExpandable used becouse of LinqKit
            var tenders = Context.Tenders.AsExpandable();
            tenders = FilterTenders(tenders, tenderQuery);
            tenders = SortTenders(tenders);
            return await GetTendersPage(tenders, pageNumber, pageSize);
        }

        public async Task<TenderDTO> GetTender(Guid guid)
        {
            var tender = await Context.Tenders
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
                //.Include(t => t.Questions.Select(q => q.Author))
                //.Include(t => t.Questions.Select(q => q.Author.AllIdentifiers))
                .FirstOrDefaultAsync(t => t.Guid == guid);

            return tender.ToDTO();
        }

        public async Task<IEnumerable<LotDTO>> GetLots(int tenderId)
        {
            var lots = await Context.Lots.Where(m => m.TenderId == tenderId).ToListAsync();
            return lots.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<LotDTO>> GetLots(Guid tenderGuid)
        {
            var lots = await Context.Lots.Where(m => m.Tender.Guid == tenderGuid).ToListAsync();
            return lots.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<ItemDTO>> GetItems(Guid tenderGuid)
        {
            var items = await Context.Items.Where(m => m.Tender.Guid == tenderGuid).ToListAsync();
            return items.Select(m => m.ToDTO());
            ;
        }

        public async Task<ItemDTO> GetItem(Guid tenderGuid, string itemId)
        {
            var item = await Context.Items.FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid);
            return item.ToDTO();
        }

        public async Task<IEnumerable<DocumentDTO>> GetTenderDocuments(int tenderId)
        {
            var documents = await Context.TenderDocuments.Where(m => m.TenderId == tenderId).ToListAsync();
            return documents.Select(m => m.ToDTO());
        }

        public async Task EditTender(TenderDTO tenderDTO)
        {
            if (!AccessManager.CanCreateTender)
                throw new AccessViolationException();
            string acc_token = await AccessManager.GetTenderTokenAsync(tenderDTO.Guid);
            using (var service = new TendersService())
            {
                var changes = await service.ChangeTender(tenderDTO.Guid, tenderDTO.DropComplexProperties(), acc_token);
                //await Context.TryUpdateTender(changes);
            }
        }

        public async Task AddItem(Guid tenderGuid, ItemDTO itemDTO)
        {
            if (!AccessManager.CanCreateTender)
                throw new AccessViolationException();
            var acc_token = await AccessManager.GetTenderTokenAsync(tenderGuid);
            var savedTender = await Context.GetTenderAsync(tenderGuid);

            var items = savedTender.Items;
            items.Add(new Item(itemDTO)
            {
                AdditionalClassifications =
                    itemDTO.AdditionalClassifications?.Select(x => new Classification(x)).ToList()
            });

            using (TendersService service = new TendersService())
            {
                var rest = await service.ChangeTender(tenderGuid, new {items}, acc_token);
                Context.UpdateTender(savedTender, rest);
            }

            await Context.SaveChangesAsync();
        }

        public async Task EditItem(Guid tenderGuid, ItemDTO itemDTO)
        {
            throw new NotImplementedException();
        }

        private IQueryable<Tender> FilterTenders(IQueryable<Tender> tenders, TenderQueryDTO filter)
        {
            return tenders
                .FilterIdentifiers(filter.ProcurementNumber)
                .FilterKeywords(filter.Keyword)
                .FilterCPV(filter.CpvCode)
                .FilterGSIN(filter.ScgsCode)
                .FilterProcurers(filter.Procurer)
                .FilterRegions(filter.Region)
                .FilterStatuses(filter.Status)
                .FilterPeriod(t => t.EnquiryPeriod, filter.ClarificationPeriod)
                .FilterPeriod(t => t.TenderPeriod, filter.ApplicationsSubmissionPeriod)
                .FilterPeriod(t => t.AuctionPeriod, filter.AuctionPeriod)
                .FilterPeriod(t => t.AwardPeriod, filter.QualificationPeriod);
        }

        private IQueryable<Tender> SortTenders(IQueryable<Tender> tenders, TendersOrder order = null)
        {
            if (order == null)
                return tenders.OrderByDescending(t => t.DateModified);
            else
            {
                // TODO 2: implement custom sorting order
                // Як мінімум повинно бути сортування хоча б по одному полю - інакше пейджинг не буде працювати
                throw new NotImplementedException("Custom sorting order is not implemented.");
            }
        }

        private async Task<IPagedList<TenderDTO>> GetTendersPage(IQueryable<Tender> tenders, int pageNumber,
            int pageSize)
        {
            return await tenders.ToPagedListAsync(pageNumber, pageSize, t => t.ToDTO());
        }

        public async Task<int> AddUserOrganization(OrganizationDTO userOrganizationDTO)
        {
            var userOrganizationModel = UserOrganizationMapper.ToModel(userOrganizationDTO);
            var userOrganization = Context.UserOrganizations.Add(userOrganizationModel);
            await Context.SaveChangesAsync();
            return userOrganization.Id;
        }

        public async Task DeleteUserOrganization(int id)
        {
            await Context.UserOrganizations.Where(o => o.Id == id).DeleteAsync();
        }

        public async Task AddQuestion(Guid tenderGuid, QuestionDTO questionDTO)
        {
            throw new NotImplementedException();
            //using (QuestionsService service = new QuestionsService(tenderGuid))
            //{
            //    var userOrganization = await Context.UserOrganizations
            //        .FirstOrDefaultAsync(m => m.Id == AccessManager.UserOrganizationId);

            //    var authorIdentifier = new Rest.Identifier(userOrganization.Identifier);

            //    var mapper = new QuestionMapper();
            //    var questionEntity = mapper.Map(questionDTO);
            //    questionEntity.Author = new QuestionAuthor(userOrganization)
            //    {
            //        AllIdentifiers = userOrganization.AllIdentifiers.Select(m => new QuestionAuthorIdentifier(m)).ToList()
            //    };

            //    var question = new Rest.Question(questionEntity)
            //    {
            //        Author = new Rest.Organization(questionEntity.Author)
            //        {
            //            Identifier = authorIdentifier
            //        }
            //    };
            //    var result = await service.AskQuestion(question);
            //}
            // TODO sync on success
            //Tender tender = await Context.Tenders.FirstAsync(t => t.Guid == tenderGuid);
            //tender.Questions.Add(new Question(questionDTO));
            //await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestions(int tenderId)
        {
            var questions = await Context.Questions.Where(m => m.TenderId == tenderId).ToListAsync();
            return questions.Select(m => m.ToDTO());
        }

        public async Task<QuestionDTO> GetQuestion(Guid tenderGuid, string questionId)
        {
            var question = await Context.Questions
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == questionId);
            return question.ToDTO();
        }

        public async Task AddAnswer(Guid tenderGuid, QuestionDTO questioDTO)
        {
            using (QuestionsService service = new QuestionsService(tenderGuid))
            {
                var result = await service
                    .AnswerQuestion(questioDTO.StringId, questioDTO.Answer,
                        "Я хз как что за токен, вставьте кто-то за меня пжалуйста.");
            }
        }

        public async Task AddLot(Guid tenderGuid, LotDTO lotDTO)
        {
            throw new NotImplementedException();
            ////TODO
            //var tender = await Context.Tenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            //var mapper = new LotMapper();
            //var lot = mapper.Map(lotDTO);
            //tender.Lots.Add(lot);
            //await Context.SaveChangesAsync();
        }

        public async Task<LotDTO> GetLot(Guid tenderGuid, string lotId)
        {
            var lot = await Context.Lots.FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == lotId);
            return lot.ToDTO();
        }

        public async Task EditLot(Guid tenderGuid, LotDTO lotDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<string> AddFeature(Guid tenderGuid, FeatureDTO featureDTO)
        {
            throw new NotImplementedException();
            //var tender = await Context.Tenders.Where(m => m.Guid == tenderGuid).FirstOrDefaultAsync();
            //var mapper = new FeatureMapper();
            //var feature = mapper.Map(featureDTO);
            //tender.Features.Add(feature);
            //await Context.SaveChangesAsync();
            //return feature.StringId;
        }

        public async Task<int> AddFeatureValue(Guid tenderGuid, string featureId, FeatureValueDTO featureValueDTO)
        {
            throw new NotImplementedException();
            //var mapper = new FeatureValueMapper();
            //var featureValue = mapper.Map(featureValueDTO);
            //Context.FeatureValues.Add(featureValue);
            //await Context.SaveChangesAsync();

            //return featureValue.Id;
        }

        public async Task<FeatureDTO> GetFeature(Guid tenderGuid, string featureStringId)
        {
            var feature = await Context.Features
                .FirstOrDefaultAsync(f => f.Tender.Guid == tenderGuid && f.StringId == featureStringId);
            return feature.ToDTO();
        }

        public async Task EditFeature(Guid tenderGuid, FeatureDTO featureDTO)
        {
            var savedFeature = await Context.Features
                .Include(f => f.Values)
                .FirstOrDefaultAsync(i => i.Tender.Guid == tenderGuid && i.StringId == featureDTO.StringId);
            // update values from DTO
            Context.Entry(savedFeature).CurrentValues.SetValues(featureDTO);
            Context.Replace(savedFeature.Values, featureDTO.Values);
            await Context.SaveChangesAsync();
        }

        public async Task<FeatureValueDTO> GetFeatureValue(Guid tenderGuid, int featureValueId)
        {
            var featureValue = await Context.FeatureValues
                .FirstOrDefaultAsync(m => m.Feature.Tender.Guid == tenderGuid && m.Id == featureValueId);
            return featureValue.ToDTO();
        }

        public async Task EditValueFeature(FeatureValueDTO featureValueDTO)
        {
            var savedFeature = await Context.FeatureValues.FirstOrDefaultAsync(m => m.Id == featureValueDTO.Id);
            Context.Entry(savedFeature).CurrentValues.SetValues(featureValueDTO);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FeatureDTO>> GetFeatures(Guid tenderGuid)
        {
            var tender = await Context.Tenders
                .Include(m => m.Features)
                .FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            return tender.Features.Select(m => m.ToDTO());
        }

        public async Task<OrganizationDTO> GetContactPoint(int userId)
        {
            var userOrganizationIdentifier = await Context.UserOrganizationIdentifiers
                .FirstOrDefaultAsync(m => m.Organization.Id == userId);
            //TODO Add Identifier

            return null;
        }

        public async Task AddUserOrganizationIdentifiers(int organizationId,
            IdentifierDTO identifierDTO)
        {
            //var mapper = new UserOrganizationIdentifierMapper();
            //var userOrganizationIdentifier = mapper.Map(userOrganizationIdentifierDTO);
            //userOrganizationIdentifier.OrganizationId = organizationId;
            //Context.UserOrganizationIdentifiers.Add(userOrganizationIdentifier);
            await Context.SaveChangesAsync();
        }

        public bool IsTenderPresent(Guid tenderGuid)
        {
            return Context.Tenders.Any(m => m.Guid == tenderGuid);
        }

        public async Task AddDocument(Guid tenderGuid, DocumentDTO tenderDocumentDTO)
        {
            throw new NotImplementedException();
            //var tender = await Context.Tenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            //var mapper = new TenderDocumentMapper();
            //var tenderDocument = mapper.Map(tenderDocumentDTO);
            //tender.Documents.Add(tenderDocument);
            //await Context.SaveChangesAsync();
        }

        public async Task<DocumentDTO> GetDocument(Guid tenderGuid, string documentId)
        {
            var document = await Context.TenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentId);
            return document.ToDTO();
        }

        public async Task EditDocument(Guid tenderGuid, DocumentDTO documentDTO)
        {
            var document = await Context.TenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentDTO.StringId);
            Context.Entry(document).CurrentValues.SetValues(documentDTO);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteDocument(Guid tenderGuid, string documentId)
        {
            var document = await Context.TenderDocuments
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == documentId);
            Context.TenderDocuments.Remove(document);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DocumentDTO>> GetDocuments(Guid tenderGuid, RelatedTo relatedTo,
            string relatedId)
        {
            var documents = await Context.TenderDocuments
                .Where(m => m.Tender.Guid == tenderGuid && m.DocumentOf == relatedTo && m.RelatedItem == relatedId)
                .ToListAsync();
            return documents.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<FeatureValueDTO>> GetFeatureValues(Guid tenderGuid, string featureStringId)
        {
            var feature = await Context.Features
                .Include(m => m.Values)
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == featureStringId);
            return feature.Values.Select(m => m.ToDTO());
        }

        public async Task<IEnumerable<ComplaintDTO>> GetTendeComplaints(Guid tenderGuid)
        {
            var tender =
                await Context.Tenders
                    .Include(m => m.Complaints)
                    .Include(m => m.Lots)
                    .FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var dtoList = tender.Complaints.Select(m => m.ToDTO());
            return dtoList;
        }

        public Task AddTenderComplaint(Guid tenderGuid, TenderComplaintDTO tenderComplaintDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ComplaintDTO> GetTenderComplaint(Guid tenderGuid, string complaintId)
        {
            var complaint = await Context.TenderComplaints
                .Include(m => m.Tender)
                .Include(m => m.Lot)
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == complaintId);
            return complaint.ToDTO();
        }

        public async Task EditTenderComplaint(TenderComplaintDTO tenderComplaintDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<LotDTO> GetLot(Guid tenderGuid, int itemLotId)
        {
            var lot = await Context.Lots
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.Id == itemLotId);
            return lot.ToDTO();
        }

        public async Task AddComplaintAnswer(Guid tenderGuid, TenderComplaintDTO complaintDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationDTO> GetTenderComplaintAuthor(Guid tenderGuid, string complaintId)
        {
            var complaint = await Context.TenderComplaints
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == complaintId);
            var complaintAuthorDTO = complaint.Author.ToDTO();
            return complaintAuthorDTO;
        }

        public async Task CheckComplaintAuthor(Guid tenderGuid, string complaintId)
        {
            var tenderComplaint = await Context.TenderComplaints
                .FirstOrDefaultAsync(m => m.Tender.Guid == tenderGuid && m.StringId == complaintId);
            var tenderComplaintAuthor = tenderComplaint.Author;
            var tenderAuthorUserOrganizationId = tenderComplaintAuthor.Identifier.OrganizationId;

            if (tenderAuthorUserOrganizationId != AccessManager.UserOrganizationId)
            {
                throw new ForbiddenException();
            }
        }

        public async Task CheckTenderAuthor(Guid tenderGuid)
        {
            var tender = await Context.Tenders
                .Include(m => m.ProcuringEntity)
                .FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var tenderAuthorUserOrganizationId = tender.ProcuringEntity.Identifier.OrganizationId;

            if (tenderAuthorUserOrganizationId != AccessManager.UserOrganizationId)
            {
                throw new ForbiddenException();
            }
        }

        public Task DeleteLot(Guid tenderGuid, string lotId)
        {
            throw new NotImplementedException();
        }

        public Task EditFeatureValue(Guid viewModelTenderGuid, FeatureValueDTO toDTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeatureValue(Guid tenderGuid, string featureId, int featureValueId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFeature(Guid tenderGuid, string featureId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItem(Guid tenderGuid, string itemId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetTenderCPVClassifications(Guid tenderGuid)
        {
            var tender = await Context.Tenders.FirstOrDefaultAsync(m => m.Guid == tenderGuid);
            var itemKeys = tender.Items
                .Select(m => m.Classification.Id)
                .GroupBy(m => m)
                .Select(m => m.Key);

            return itemKeys;
        }

        public Task<IEnumerable<string>> GetTenderGSINClassifications(Guid tenderGuid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TenderDTO>> GetTenders(int userOrganizationId)
        {
            var createdTenders = await Context.CreatedTenders
                .Where(m => m.UserOrganization.Id == userOrganizationId)
                .Select(m => m.Id).ToListAsync();

            var tenders = await Context.Tenders
                .Where(m => createdTenders.Contains(m.Id))
                .ToListAsync();
            return tenders.Select(m => m.ToDTO());
        }
    }
}