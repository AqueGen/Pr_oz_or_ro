using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Published;


namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class DocumentController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public DocumentController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/addDocument/{relatedTo}/{relatedId}")]
        [HttpGet]
        public ActionResult AddDocument(Guid tenderGuid, string relatedId, RelatedTo relatedTo)
        {
            var viewModel = new DocumentViewModel()
            {
                TenderGuid = tenderGuid,
                RelatedId = relatedId == tenderGuid.ToString("N") ? null : relatedId,
                DocumentOf = relatedTo
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addDocument/{relatedTo}/{relatedId}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddDocument(DocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var documentDTO = viewModel.ToDTO();

            await TenderProvider.AddDocument(viewModel.TenderGuid, documentDTO);

            if (viewModel.DocumentOf == RelatedTo.Tender)
            {
                return RedirectToAction("EditTender", "TenderBelowThreshold", new { tenderGuid = viewModel.TenderGuid });
            }
            else if (viewModel.DocumentOf == RelatedTo.Lot)
            {
                return RedirectToAction("EditLot", "Lot",
                    new { tenderGuid = viewModel.TenderGuid, lotId = viewModel.RelatedId });
            }
            else
            {
                return RedirectToAction("EditItem", "Item",
                    new { tenderGuid = viewModel.TenderGuid, itemId = viewModel.RelatedId });
            }
        }

        [Route("tender/{tenderGuid:guid}/Document/{documentId}/edit")]
        [HttpGet]
        public async Task<ActionResult> EditDocument(Guid tenderGuid, string documentId)
        {
            var documentDTO = await TenderProvider.GetDocument(tenderGuid, documentId);

            var viewModel = new DocumentViewModel(tenderGuid, documentDTO)
            {
                TenderGuid = tenderGuid
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/Document/{documentId}/edit")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> EditDocument(DocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var documentDTO = viewModel.ToDTO();
            await TenderProvider.EditDocument(viewModel.TenderGuid, documentDTO);

            if (viewModel.DocumentOf == RelatedTo.Tender)
            {
                return RedirectToAction("EditTender", "TenderBelowThreshold", new { tenderGuid = viewModel.TenderGuid });
            }
            else if (viewModel.DocumentOf == RelatedTo.Lot)
            {
                return RedirectToAction("EditLot", "Lot",
                    new { tenderGuid = viewModel.TenderGuid, lotId = viewModel.RelatedId });
            }
            else if (viewModel.DocumentOf == RelatedTo.Item)
            {
                return RedirectToAction("EditItem", "Item",
                    new { tenderGuid = viewModel.TenderGuid, itemId = viewModel.RelatedId });
            }
            else
            {
                return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
            }
        }

        [Route("tender/{tenderGuid:guid}/Document/{documentId}/delete")]
        [HttpPost]
        public async Task<ActionResult> DeleteDocument(Guid tenderGuid, string documentId)
        {
            await TenderProvider.DeleteDocument(tenderGuid, documentId);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = tenderGuid });
        }
    }
}