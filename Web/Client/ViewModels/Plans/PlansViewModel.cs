using Kapitalist.Data.Models.DTO;
using PagedList;

namespace Kapitalist.Web.Client.ViewModels.Plans
{
    public class PlansViewModel
    {
        public PlanQueryViewModel Query;
        public IPagedList<PlanDTO> Plans;

        public PlansViewModel(IPagedList<PlanDTO> plans, PlanQueryViewModel query)
        {
            Query = query;
            Plans = plans;
        }
    }
}