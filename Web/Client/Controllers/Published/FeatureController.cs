using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class FeatureController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public FeatureController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/addFeature/{type}/{relatedId}")]
        [HttpGet]
        public async Task<ActionResult> AddFeature(Guid tenderGuid, FeatureType type, string relatedId)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);

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
            var featureStringId = await TenderProvider.AddFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editFeature/{featureId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeature(Guid tenderGuid, string featureId)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);
            var feature = await TenderProvider.GetFeature(tenderGuid, featureId);

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
            await TenderProvider.EditFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/deleteFeature/{featureId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFeature(Guid tenderGuid, string featureId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditFeature",
                    new { tenderGuid, featureId });
            }

            await TenderProvider.DeleteFeature(tenderGuid, featureId);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid });
        }


        [Route("tender/{tenderGuid:guid}/addFeatureEU/{type}/{relatedId}")]
        [HttpGet]
        public async Task<ActionResult> AddFeatureEU(Guid tenderGuid, FeatureType type, string relatedId)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);

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
            var featureStringId = await TenderProvider.AddFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editFeatureEU/{featureId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeatureEU(Guid tenderGuid, string featureId)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);
            var feature = await TenderProvider.GetFeature(tenderGuid, featureId);

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
            await TenderProvider.EditFeature(viewModel.TenderGuid, featureDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }
    }
}