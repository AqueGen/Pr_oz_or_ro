using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    public class DraftPlanController : BaseDraftController
    {
        private IDraftPlanProvider DraftPlanProvider { get; }

        public DraftPlanController(IDraftPlanProvider draftPlanProvider, Lazy<IAccessManager> accessManager) : base(accessManager)
        {
            DraftPlanProvider = draftPlanProvider;
        }

        [HttpGet]
        public ActionResult AddItem(Guid planGuid)
        {
            var viewModel = new DraftPlanItemViewModel
            {
                PlanGuid = planGuid
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddItem(DraftPlanItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            ItemDTO draftPlanItemDTO = viewModel.ToDTO();
            await DraftPlanProvider.AddItem(viewModel.PlanGuid, draftPlanItemDTO);

            return RedirectToAction("Items", new { planGuid = viewModel.PlanGuid });
        }

        [HttpGet]
        public ActionResult AddPlan()
        {
            var viewModel = new DraftPlanViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddPlan(DraftPlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            int organizationId = AccessManager.Value.UserOrganizationId;
            var draftPlanDTO = viewModel.ToDTO();
            await DraftPlanProvider.AddPlan(organizationId, draftPlanDTO);

            return RedirectToAction("Items", new { planGuid = viewModel.Guid });
        }

        [HttpGet]
        public async Task<ActionResult> EditItem(Guid planGuid, string itemId)
        {
            var item = await DraftPlanProvider.GetItem(planGuid, itemId);

            var viewModel = new DraftPlanItemViewModel(planGuid, item);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditItem(DraftPlanItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var draftPlanItemDTO = viewModel.ToDTO();
            await DraftPlanProvider.EditItem(viewModel.PlanGuid, draftPlanItemDTO);

            return RedirectToAction("Items", new { planGuid = viewModel.PlanGuid });
        }

        [HttpGet]
        public async Task<ActionResult> EditPlan(Guid planGuid)
        {
            var draftPlanDTO = await DraftPlanProvider.GetPlan(planGuid);
            var viewModel = new DraftPlanViewModel(draftPlanDTO);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditPlan(DraftPlanViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var draftPlanDTO = viewModel.ToDTO();
            await DraftPlanProvider.EditPlan(draftPlanDTO);

            return RedirectToAction("Items", new { planGuid = viewModel.Guid });
        }

        public ActionResult Index()
        {
            return RedirectToAction("AddPlan");
        }

        [HttpGet]
        public async Task<ActionResult> Items(Guid planGuid)
        {
            var viewModel = new DraftPlanItemsViewModel
            {
                PlanGuid = planGuid
            };

            var items = await DraftPlanProvider.GetItems(planGuid);
            viewModel.Items = items.Select(m => new DraftPlanItemViewModel(m));

            return View(viewModel);
        }

        public ActionResult TotalInformation(Guid planGuid)
        {
            throw new NotImplementedException();
        }
    }
}