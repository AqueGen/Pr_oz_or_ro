using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using PagedList;

namespace Kapitalist.Services.Prozorro.Providers
{
    public interface IPlanProvider
    {
        Task<IPagedList<PlanDTO>> GetPlansPage(int pageNumber, int pageSize = 10);

        Task<IPagedList<PlanDTO>> GetPlansPage(PlanQueryDTO query, int pageNumber, int pageSize = 10);

        Task<PlanDTO> GetPlan(Guid planGuid);

        Task EditPlan(PlanDTO draftPlanDTO);

        Task<IEnumerable<ItemDTO>> GetItems(Guid planGuid);

        Task AddItem(Guid planGuid, ItemDTO planItemDTO);

        Task<ItemDTO> GetItem(Guid planGuid, string itemId);

        Task EditItem(Guid planGuid, ItemDTO planItemDTO);
    }
}