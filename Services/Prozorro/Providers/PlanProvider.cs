using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Data.Store;
using Kapitalist.Data.Store.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Services.Prozorro.Helpers;
using Kapitalist.Services.Prozorro.Providers.Models;
using Kapitalist.Web.Security;
using LinqKit;
using PagedList;
using Kapitalist.Services.Prozorro.Mappers;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class PlanProvider : BaseProvider, IPlanProvider
    {
        public PlanProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task<IPagedList<PlanDTO>> GetPlansPage(int pageNumber, int pageSize = 10)
        {
            var plans = Context.Plans.AsQueryable();
            // Сортування обов'язкове.
            // Пейджинг не працює на невідсортованому списку.
            plans = SorPlans(plans);
            return await GetPlansPage(plans, pageNumber, pageSize);
        }

        public async Task<IPagedList<PlanDTO>> GetPlansPage(PlanQueryDTO query, int pageNumber, int pageSize = 10)
        {
            // AsExpandable used becouse of LinqKit
            var plans = Context.Plans.AsExpandable();
            plans = FilterPlans(plans, query);
            plans = SorPlans(plans);
            return await GetPlansPage(plans, pageNumber, pageSize);
        }

        private async Task<IPagedList<PlanDTO>> GetPlansPage(IQueryable<Plan> plans, int pageNumber, int pageSize)
        {
            return await plans.ToPagedListAsync(pageNumber, pageSize, m => m.ToDTO());
        }

        private IQueryable<Plan> FilterPlans(IQueryable<Plan> plans, PlanQueryDTO filter)
        {
            return plans;
        }

        private IQueryable<Plan> SorPlans(IQueryable<Plan> tenders, TendersOrder order = null)
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

        public async Task<PlanDTO> GetPlan(Guid planGuid)
        {
            var plan = await Context.Plans.FirstOrDefaultAsync(m => m.Guid == planGuid);
            return plan.ToDTO();
        }

        public async Task EditPlan(PlanDTO draftPlanDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ItemDTO>> GetItems(Guid planGuid)
        {
            var planItems = await Context.PlanItems.Where(m => m.Plan.Guid == planGuid).ToListAsync();
            return planItems.Select(m => m.ToDTO());
        }

        public async Task AddItem(Guid planGuid, ItemDTO planItemDTO)
        {
            throw new NotImplementedException();
            //var plan = await Context.Plans.FirstOrDefaultAsync(m => m.Guid == planGuid);
            //var mapper = new PlanItemMapper();
            //var planItem = mapper.Map(planItemDTO);
            //plan.Items.Add(planItem);
            //await Context.SaveChangesAsync();
        }

        public async Task<ItemDTO> GetItem(Guid planGuid, string itemId)
        {
            var planItem = await Context.PlanItems.FirstOrDefaultAsync(m => m.Plan.Guid == planGuid && m.StringId == itemId);
            return planItem.ToDTO();
        }

        public async Task EditItem(Guid planGuid, ItemDTO planItemDTO)
        {
            var plan = await Context.Plans.Include(m => m.Items).FirstOrDefaultAsync(m => m.Guid == planGuid);
        }
    }
}