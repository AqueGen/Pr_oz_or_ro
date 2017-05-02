using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.ViewModels.Plans;
using PagedList;

namespace Kapitalist.Web.Client.Controllers
{
    public class PlansController : BaseController
    {
        private Lazy<IPlanProvider> PlanProvider { get; }

        public PlansController(Lazy<IPlanProvider> planProvider)
        {
            PlanProvider = planProvider;
        }

        public PartialViewResult GetTemplate(string template)
        {
            return PartialView($"Searches/QueryComponents/{template}Partial");
        }

        [HttpGet]
        public async Task<ActionResult> Index(PlanQueryViewModel viewModel)
        {
            if (viewModel.PageNumber < 1)
                viewModel.PageNumber = 1;
            if (viewModel.PageSize < 1)
                viewModel.PageSize = 10;

            PlanQueryDTO filter = new PlanQueryDTO
            {
                Keyword = viewModel.Keyword,
                CpvCode = viewModel.CpvCode,
                ScgsCode = viewModel.GsinCode,
                PlanNumbers = viewModel.PlanNumbers,
                Procurer = viewModel.Procurer,
                Region = viewModel.Region,
                ProcedurePeriod = viewModel.ProcedurePeriod?.ToDTO(),
                ProcedureType = viewModel.ProcedureType
            };

            IPagedList<PlanDTO> plansPagedList =
                await PlanProvider.Value.GetPlansPage(filter, viewModel.PageNumber, viewModel.PageSize);

            var plansViewModel = new PlansViewModel(plansPagedList, viewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Searches/Plans/PlanTable", plansViewModel);
            }
            else
            {
                return View("Plans", plansViewModel);
            }
        }
    }
}