using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftLotController : BaseDraftController
    {
        private IDraftProvider DraftProvider { get; }



        public DraftLotController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }


        [Route("tender/{tenderGuid:guid}/addLot")]
        [HttpGet]
        public ActionResult AddLot(Guid tenderGuid)
        {
            var viewModel = new DraftLotViewModel(tenderGuid);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addLot")]
        [HttpPost]
        public async Task<ActionResult> AddLot(DraftLotViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftLotDTO = viewModel.ToDraftDTO();
            await DraftProvider.AddDraftLot(viewModel.TenderGuid, draftLotDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/addLotEU")]
        [HttpGet]
        public ActionResult AddLotEU(Guid tenderGuid)
        {
            var viewModel = new DraftLotEUViewModel(tenderGuid);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addLotEU")]
        [HttpPost]
        public async Task<ActionResult> AddLotEU(DraftLotEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftLotDTO = viewModel.ToDraftDTO();
            await DraftProvider.AddDraftLot(viewModel.TenderGuid, draftLotDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editLot/{lotId}")]
        [HttpGet]
        public async Task<ActionResult> EditLot(Guid tenderGuid, string lotId)
        {
            var lotDTO = await DraftProvider.GetDraftLot(tenderGuid, lotId);
            var viewModel = new DraftLotViewModel(tenderGuid, lotDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editLotEU/{lotId}")]
        [HttpGet]
        public async Task<ActionResult> EditLotEU(Guid tenderGuid, string lotId)
        {
            var lotDTO = await DraftProvider.GetDraftLot(tenderGuid, lotId);
            var viewModel = new DraftLotEUViewModel(tenderGuid, lotDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editLot/{lotId}")]
        [HttpPost]
        public async Task<ActionResult> EditLot(DraftLotViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftLotDTO = viewModel.ToDraftDTO();
            await DraftProvider.EditDraftLot(viewModel.TenderGuid, draftLotDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }


        [Route("tender/{tenderGuid:guid}/editLotEU/{lotId}")]
        [HttpPost]
        public async Task<ActionResult> EditLotEU(DraftLotEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftLotDTO = viewModel.ToDraftDTO();
            await DraftProvider.EditDraftLot(viewModel.TenderGuid, draftLotDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/deleteLot/{lotId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteLot(Guid tenderGuid, string lotId)
        {
            await DraftProvider.DeleteLot(tenderGuid, lotId);
            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = tenderGuid });
        }
    }
}