using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Kapitalist.Data.Store;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Web.Security;
using Kapitalist.Services.Prozorro.Helpers;
using Kapitalist.Services.Prozorro.Mappers;

namespace Kapitalist.Services.Prozorro.Providers
{
    public class DraftPlanProvider : BaseProvider, IDraftPlanProvider
    {
        public DraftPlanProvider(StoreContext context, IAccessManager accessManager)
            : base(context, accessManager)
        {
        }

        public async Task AddPlan(int organizationId, DraftPlanDTO draftPlanDTO)
        {
            var draftPlan = draftPlanDTO.ToDraftPlan();
            draftPlan.ProcuringEntityId = organizationId;
            Context.DraftPlans.Add(draftPlan);
            await Context.SaveChangesAsync();
        }

        public async Task<DraftPlanDTO> GetPlan(Guid planGuid)
        {
            var draftPlan = await Context.DraftPlans.FirstOrDefaultAsync(m => m.Guid == planGuid);
            return draftPlan.ToDTO();
        }

        public async Task EditPlan(DraftPlanDTO draftPlanDTO)
        {
            var savedPlan = await Context.DraftPlans.FirstOrDefaultAsync(m => m.Guid == draftPlanDTO.Guid);
            Context.Entry(savedPlan).CurrentValues.SetValues(draftPlanDTO);
            //TODO посмотреть Budget.Project обьект, во вью он не отображается
            await Context.SaveChangesAsync();
        }

        public async Task<int> AddItem(Guid planGuid, ItemDTO draftPlanItemDTO)
        {
            var planId = await Context.DraftPlans.Where(m => m.Guid == planGuid).Select(m => m.Id).FirstOrDefaultAsync();
            var draftPlanItem = draftPlanItemDTO.ToDraftPlanItem();
            Context.DraftPlanItems.Add(draftPlanItem);
            await Context.SaveChangesAsync();
            return draftPlanItem.Id;
        }

        public async Task<IEnumerable<ItemDTO>> GetItems(Guid planGuid)
        {
            var plan = await Context.DraftPlans
                .Include(m => m.Items)
                .FirstOrDefaultAsync(m => m.Guid == planGuid);
            return plan.Items.Select(m => m.ToDTO());
        }

        public async Task<ItemDTO> GetItem(Guid planGuid, string itemId)
        {
            var draftPlanItem =
                await Context.DraftPlanItems.FirstOrDefaultAsync(m => m.Plan.Guid == planGuid && m.StringId == itemId);

            return draftPlanItem.ToDTO();
        }

        public async Task EditItem(Guid planGuid, ItemDTO draftPlanItemDTO)
        {
            var plan = await Context.DraftPlans.FirstOrDefaultAsync(m => m.Guid == planGuid);

            var savedPlan =
                await
                    Context.DraftPlanItems.FirstOrDefaultAsync(
                        m => m.PlanId == plan.Id && m.StringId == draftPlanItemDTO.StringId);
            Context.Entry(savedPlan).CurrentValues.SetValues(draftPlanItemDTO);
            await Context.SaveChangesAsync();
        }
    }
}