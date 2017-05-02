using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class PlanController : BasePublishedController
    {
        private IPlanProvider PlanProvider { get; }

        public PlanController(IPlanProvider planProvider)
        {
            PlanProvider = planProvider;
        }

        [HttpGet]
        public ActionResult AddItem(Guid planGuid)
        {
            var viewModel = new PlanItemViewModel
            {
                PlanGuid = planGuid
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(PlanItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

                var draftPlanItemDTO = viewModel.ToDTO();
                await PlanProvider.AddItem(viewModel.PlanGuid, draftPlanItemDTO);

                return RedirectToAction("Info", "TenderInfo", new { planGuid = viewModel.PlanGuid });
        }

        [HttpGet]
        public async Task<ActionResult> EditItem(Guid planGuid, string itemId)
        {
                var item = await PlanProvider.GetItem(planGuid, itemId);

                var viewModel = new PlanItemViewModel(planGuid, item);
                return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditItem(PlanItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

                var draftPlanItemDTO = viewModel.ToDTO();
                await PlanProvider.EditItem(viewModel.PlanGuid, draftPlanItemDTO);

                return RedirectToAction("Info", "TenderInfo", new { planGuid = viewModel.PlanGuid });
        }

        [HttpGet]
        public async Task<ActionResult> EditPlan(Guid planGuid)
        {
                var draftPlanDTO = await PlanProvider.GetPlan(planGuid);
                var viewModel = new PlanViewModel(draftPlanDTO);
                return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditPlan(PlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

                var draftPlanDTO = viewModel.ToDTO();
                await PlanProvider.EditPlan(draftPlanDTO);

                return RedirectToAction("Info", "TenderInfo", new {planGuid = viewModel.Guid});
        }

    }
}