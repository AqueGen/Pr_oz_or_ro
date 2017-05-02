using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Published;


namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class LotController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public LotController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/addLot")]
        [HttpGet]
        public ActionResult AddLot(Guid tenderGuid)
        {
            var viewModel = new LotViewModel(tenderGuid);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addLot")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLot(LotViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var lotDTO = viewModel.ToModelDTO();
            await TenderProvider.AddLot(viewModel.TenderGuid, lotDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/addLotEU")]
        [HttpGet]
        public ActionResult AddLotEU(Guid tenderGuid)
        {
            var viewModel = new LotEUViewModel(tenderGuid);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addLotEU")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddLotEU(LotEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var lotDTO = viewModel.ToModelDTO();
            await TenderProvider.AddLot(viewModel.TenderGuid, lotDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editLot/{lotId}")]
        [HttpGet]
        public async Task<ActionResult> EditLot(Guid tenderGuid, string lotId)
        {
            var lotDTO = await TenderProvider.GetLot(tenderGuid, lotId);
            var viewModel = new LotViewModel(tenderGuid, lotDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editLotEU/{lotId}")]
        [HttpGet]
        public async Task<ActionResult> EditLotEU(Guid tenderGuid, string lotId)
        {
            var lotDTO = await TenderProvider.GetLot(tenderGuid, lotId);
            var viewModel = new LotEUViewModel(tenderGuid, lotDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editLot/{lotId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLot(LotViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            
            var lotDTO = viewModel.ToModelDTO();
            await TenderProvider.EditLot(viewModel.TenderGuid, lotDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }


        [Route("tender/{tenderGuid:guid}/editLotEU/{lotId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditLotEU(LotEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var lotDTO = viewModel.ToModelDTO();
            await TenderProvider.EditLot(viewModel.TenderGuid, lotDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/deleteLot/{lotId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteLot(Guid tenderGuid, string lotId)
        {
            await TenderProvider.DeleteLot(tenderGuid, lotId);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = tenderGuid });
        }
    }
}