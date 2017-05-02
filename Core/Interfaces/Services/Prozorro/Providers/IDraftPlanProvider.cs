using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kapitalist.Data.Models.DTO;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface IDraftPlanProvider
    {
        Task AddPlan(int organizationId, DraftPlanDTO draftPlanDTO);
        Task<DraftPlanDTO> GetPlan(Guid planGuid);
        Task EditPlan(DraftPlanDTO draftPlanDTO);
        Task<int> AddItem(Guid planGuid, ItemDTO draftPlanItemDTO);
        Task<IEnumerable<ItemDTO>>  GetItems(Guid planGuid);
        Task<ItemDTO> GetItem(Guid planGuid, string itemId);
        Task EditItem(Guid planGuid, ItemDTO draftPlanItemDTO);
    }
}