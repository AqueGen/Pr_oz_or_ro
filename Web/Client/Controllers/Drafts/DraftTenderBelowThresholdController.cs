using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft/belowThreshold")]
    public class DraftTenderBelowThresholdController : BaseDraftController
    {
        private IDraftProvider DraftProvider { get; }

        public DraftTenderBelowThresholdController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }

        [Route("createTender")]
        [HttpGet]
        public ActionResult AddTender()
        {
            var viewModel = new DraftTenderBelowThresholdViewModel();
            return View(viewModel);
        }

        [Route("createTender")]
        [HttpPost]
        public async Task<ActionResult> AddTender(DraftTenderBelowThresholdViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var tenderDTO = viewModel.ToDTO();
            tenderDTO.Guid = await DraftProvider.AddDraftTender(tenderDTO);
            return RedirectToAction("Info", "DraftTenderInfo",  new {tenderGuid = tenderDTO.Guid});
        }

        [Route("editTender/{tenderGuid:guid}")]
        [HttpGet]
        public async Task<ActionResult> EditTender(Guid tenderGuid)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            tender.Documents = tender.Documents.Where(m => m.DocumentOf == RelatedTo.Tender).ToList();
            var viewModel = new DraftTenderBelowThresholdViewModel(tender);

            return View(viewModel);
        }

        [Route("editTender/{tenderGuid:guid}")]
        [HttpPost]
        public async Task<ActionResult> EditTender(DraftTenderBelowThresholdViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftTender(viewModel.Guid, draftDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.Guid});
        }

        [Route("deleteTender/{tenderGuid:Guid}")]
        [HttpPost]
        public async Task<ActionResult> DeleteTender(Guid tenderGuid)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = tenderGuid});
            }

            await DraftProvider.DeleteTender(tenderGuid);
            return RedirectToAction("Index", "Tenders");
        }
    }
}