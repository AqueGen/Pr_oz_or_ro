using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Client.ViewModels.Published;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftItemController : BaseDraftController
    {
        public DraftItemController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
        }

        private IDraftProvider DraftProvider { get; }

        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItem")]
        [Route("tender/{tenderGuid:guid}/addItem")]
        [HttpGet]
        public async Task<ActionResult> AddItem(Guid tenderGuid, string lotId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var viewModel = new DraftItemViewModel
            {
                TenderGuid = tenderGuid,
                LotStringId = lotId,
                ProcurementMethodType = tender.ProcurementMethodType
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItem")]
        [Route("tender/{tenderGuid:guid}/addItem")]
        [HttpPost]
        public async Task<ActionResult> AddItem(DraftItemViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await DraftProvider.AddDraftItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/editItem/{itemId}")]
        [HttpGet]
        public async Task<ActionResult> EditItem(Guid tenderGuid, string itemId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var item = await DraftProvider.GetDraftItem(tenderGuid, itemId);
            var viewModel = new DraftItemViewModel(tenderGuid, item)
            {
                ProcurementMethodType = tender.ProcurementMethodType
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editItem/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> EditItem(DraftItemViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/deleteItem/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteItem(Guid tenderGuid, string itemId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditItem", new {tenderGuid, itemId});
            }

            await DraftProvider.DeleteItem(tenderGuid, itemId);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid});
        }


        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItemEU")]
        [HttpGet]
        public async Task<ActionResult> AddItemEU(Guid tenderGuid, string lotId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var viewModel = new DraftItemEUViewModel
            {
                TenderGuid = tenderGuid,
                LotStringId = lotId,
                ProcurementMethodType = tender.ProcurementMethodType
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItemEU")]
        [HttpPost]
        public async Task<ActionResult> AddItemEU(DraftItemEUViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await DraftProvider.AddDraftItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/editItemEU/{itemId}")]
        [HttpGet]
        public async Task<ActionResult> EditItemEU(Guid tenderGuid, string itemId)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            var item = await DraftProvider.GetDraftItem(tenderGuid, itemId);
            var viewModel = new DraftItemEUViewModel(tenderGuid, item)
            {
                ProcurementMethodType = tender.ProcurementMethodType
            };
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editItemEU/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> EditItemEU(DraftItemEUViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await DraftProvider.EditDraftItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "DraftTenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        private async Task CheckCPVGroupNumber(Guid tenderGuid, ClassificationViewModel lot)
        {
            var cpvList = await DraftProvider.GetTenderCPVClassifications(tenderGuid);

            int firstSymbol = 3;
            if (lot.Id.Length > firstSymbol)
            {
                string lotGroup = lot.Id.Substring(0, firstSymbol);

                bool isTrue = cpvList.All(m => m.Substring(0, firstSymbol) == lotGroup);
                if (!isTrue)
                {
                    string currentCPV = cpvList.FirstOrDefault().Substring(0, firstSymbol);
                    ModelState.AddModelError("Classification.Id",
                        $"Перші 3-и символи класифікатору повинні співпадати для всіх закупівель. Оберіть код який починається на '{currentCPV}', або змініть класифікатор для всіх об'єктів тендеру.");
                }
            }
        }
    }
}