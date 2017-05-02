using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftFeatureController : BaseDraftController
    {
        public DraftFeatureController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }

        private IDraftProvider DraftProvider { get; }

        [Route("tender/{tenderGuid:guid}/addFeature/{type}/{relatedId}")]
        [HttpGet]
        public async Task<ActionResult> AddFeature(Guid tenderGuid, FeatureType type, string relatedId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var viewModel = new FeatureViewModel
            {
                TenderGuid = tenderGuid,
                RelatedItem = relatedId,
                Type = type
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addFeature/{type}/{relatedId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFeature(FeatureViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var featureDTO = viewModel.ToDTO();
            var featureStringId = await DraftProvider.AddDraftFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/editFeature/{featureId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeature(Guid tenderGuid, string featureId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);
            var feature = await DraftProvider.GetDraftFeature(tenderGuid, featureId);

            var viewModel = new FeatureViewModel(tenderGuid, feature);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editFeature/{featureId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFeature(FeatureViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var featureDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/deleteFeature/{featureId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFeature(Guid tenderGuid, string featureId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditFeature",
                    new {tenderGuid, featureId});
            }

            await DraftProvider.DeleteFeature(tenderGuid, featureId);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/addFeatureEU/{type}/{relatedId}")]
        [HttpGet]
        public async Task<ActionResult> AddFeatureEU(Guid tenderGuid, FeatureType type, string relatedId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var viewModel = new FeatureEUViewModel
            {
                TenderGuid = tenderGuid,
                RelatedItem = relatedId,
                Type = type
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addFeatureEU/{type}/{relatedId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFeatureEU(FeatureEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var featureDTO = viewModel.ToDTO();
            var featureStringId = await DraftProvider.AddDraftFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/editFeatureEU/{featureId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeatureEU(Guid tenderGuid, string featureId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);
            var feature = await DraftProvider.GetDraftFeature(tenderGuid, featureId);

            var viewModel = new FeatureEUViewModel(tenderGuid, feature);

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editFeatureEU/{featureId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFeatureEU(FeatureEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var featureDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }
    }
}