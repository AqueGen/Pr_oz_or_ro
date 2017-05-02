using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Resources;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [RoutePrefix("draft")]
    public class DraftFeatureValueController : BaseDraftController
    {
        public DraftFeatureValueController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }

        private IDraftProvider DraftProvider { get; }

        [Route("tender/{tenderGuid:guid}/feature/{featureId}/addFeatureValue")]
        [HttpGet]
        public ActionResult AddFeatureValue(Guid tenderGuid, string featureId)
        {
            var viewModel = new FeatureValueViewModel
            {
                TenderGuid = tenderGuid,
                FeatureStringId = featureId
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/feature/{featureId}/addFeatureValue")]
        [HttpPost]
        public async Task<ActionResult> AddFeatureValue(FeatureValueViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var featureValuesPresent =
                await DraftProvider.GetDraftFeatureValues(viewModel.TenderGuid, viewModel.FeatureStringId);
            var featureValuesSum = featureValuesPresent.Sum(m => m.Value);
            if (viewModel.Value + featureValuesSum > 0.3)
            {
                ModelState.AddModelError(nameof(viewModel.Value), GlobalRes.MaximumNonPriceCriteriaMessage);
                return View(viewModel);
            }
            var draftFeatureValueDTO = viewModel.ToDTO();
            await
                DraftProvider.AddDraftFeatureValue(viewModel.TenderGuid, viewModel.FeatureStringId, draftFeatureValueDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/feature/{featureId}/editFeatureValue/{featureValueId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeatureValue(Guid tenderguId, string featureId, int featureValueId)
        {
            var featureValue = await DraftProvider.GetDraftFeatureValue(tenderguId, featureValueId);
            var viewModel = new FeatureValueViewModel(tenderguId, featureValue);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/feature/{featureId}/editFeatureValue/{featureValueId}")]
        [HttpPost]
        public async Task<ActionResult> EditFeatureValue(FeatureValueViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await DraftProvider.EditDraftFeatureValue(viewModel.TenderGuid, viewModel.ToDTO());

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/feature/{featureId}/addFeatureValueEU")]
        [HttpGet]
        public ActionResult AddFeatureValueEU(Guid tenderGuid, string featureId)
        {
            var viewModel = new FeatureValueEUViewModel
            {
                TenderGuid = tenderGuid,
                FeatureStringId = featureId
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/feature/{featureId}/addFeatureValueEU")]
        [HttpPost]
        public async Task<ActionResult> AddFeatureValueEU(FeatureValueEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var featureValuesPresent =
                await DraftProvider.GetDraftFeatureValues(viewModel.TenderGuid, viewModel.FeatureStringId);
            var featureValuesSum = featureValuesPresent.Sum(m => m.Value);
            if (viewModel.Value + featureValuesSum > 0.3)
            {
                ModelState.AddModelError(nameof(viewModel.Value), GlobalRes.MaximumNonPriceCriteriaMessage);
                return View(viewModel);
            }
            var draftFeatureValueDTO = viewModel.ToDTO();
            await
                DraftProvider.AddDraftFeatureValue(viewModel.TenderGuid, viewModel.FeatureStringId, draftFeatureValueDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/feature/{featureId}/editFeatureValueEU/{featureValueId}")]
        [HttpGet]
        public async Task<ActionResult> EditFeatureValueEU(Guid tenderguId, string featureId, int featureValueId)
        {
            var featureValue = await DraftProvider.GetDraftFeatureValue(tenderguId, featureValueId);
            var viewModel = new FeatureValueEUViewModel(tenderguId, featureValue);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/feature/{featureId}/editFeatureValueEU/{featureValueId}")]
        [HttpPost]
        public async Task<ActionResult> EditFeatureValueEU(FeatureValueEUViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await DraftProvider.EditDraftFeatureValue(viewModel.TenderGuid, viewModel.ToDTO());

            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/feature/{featureId}/deleteFeatureValue/{featureValueId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteFeatureValue(Guid tenderGuid, string featureId, int featureValueId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditFeatureValue", new
                {
                    tenderGuid,
                    featureStringId = featureId,
                    featureValueId
                });
            }

            await DraftProvider.DeleteFeatureValue(tenderGuid, featureId, featureValueId);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid});
        }
    }
}