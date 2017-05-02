using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftDocumentController : BaseDraftController
    {
        private IDraftProvider DraftProvider { get; }

        public DraftDocumentController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }

        [Route("tender/{tenderGuid:guid}/addDocument/{relatedTo}/{relatedId}")]
        [HttpGet]
        public ActionResult AddDocument(Guid tenderGuid, string relatedId, RelatedTo relatedTo)
        {
            var viewModel = new DraftDocumentViewModel()
            {
                TenderGuid = tenderGuid,
                RelatedId = relatedId == tenderGuid.ToString("N") ? null : relatedId,
                DocumentOf = relatedTo
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addDocument/{relatedTo}/{relatedId}")]
        [HttpPost]
        public async Task<ActionResult> AddDocument(DraftDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var documentDTO = viewModel.ToDTO();

            await DraftProvider.AddDraftDocument(viewModel.TenderGuid, documentDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editDocument/{documentId}")]
        [HttpGet]
        public async Task<ActionResult> EditDocument(Guid tenderGuid, string documentId)
        {
            var draftTenderDocumentDTO = await DraftProvider.GetDraftDocument(tenderGuid, documentId);

            var viewModel = new DraftDocumentViewModel(tenderGuid, draftTenderDocumentDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editDocument/{documentId}")]
        [HttpPost]
        public async Task<ActionResult> EditDocument(DraftDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var documentDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftDocument(viewModel.TenderGuid, documentDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/deleteDocument/{documentId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteDocument(Guid tenderGuid, string documentId)
        {
            await DraftProvider.DeleteDraftDocument(tenderGuid, documentId);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = tenderGuid});
        }
    }
}