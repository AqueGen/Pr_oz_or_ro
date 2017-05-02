using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.Controllers.Reals;
using Kapitalist.Web.Client.ViewModels.Plan;

namespace Kapitalist.Web.Client.Controllers
{
    public class PlanController : BaseRealController
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

                return RedirectToAction("Items", new { planGuid = viewModel.PlanGuid });
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

                return RedirectToAction("Items", new { planGuid = viewModel.PlanGuid });
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

                return RedirectToAction("Items", new {planGuid = viewModel.Guid});
        }

        [HttpGet]
        public async Task<ActionResult> Items(Guid planGuid)
        {
                var viewModel = new PlanItemsViewModel
                {
                    PlanGuid = planGuid
                };

                var items = await PlanProvider.GetItems(planGuid);
                viewModel.Items = items.Select(m => new PlanItemViewModel(m));

                return View(viewModel);
        }
        public ActionResult TotalInformation(Guid planGuid)
        {
            throw new NotImplementedException();
        }
    }
}