using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Published;

namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class ItemController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public ItemController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/addItem")]
        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItem")]
        [HttpGet]
        public async Task<ActionResult> AddItem(Guid tenderGuid, string lotId)
        {
            var viewModel = new ItemViewModel
            {
                TenderGuid = tenderGuid,
                LotStringId = lotId
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addItem")]
        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItem")]
        [HttpPost]
        public async Task<ActionResult> AddItem(ItemViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await TenderProvider.AddItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editItem/{itemId}")]
        [HttpGet]
        public async Task<ActionResult> EditItem(Guid tenderGuid, string itemId)
        {
            var item = await TenderProvider.GetItem(tenderGuid, itemId);
            var viewModel = new ItemViewModel(tenderGuid, item);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editItem/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> EditItem(ItemViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await TenderProvider.EditItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/deleteItem/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> DeleteItem(Guid tenderGuid, string itemId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditItem", new { tenderGuid, itemId });
            }

            await TenderProvider.DeleteItem(tenderGuid, itemId);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid });
        }


        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItemEU")]
        [HttpGet]
        public async Task<ActionResult> AddItemEU(Guid tenderGuid, string lotId)
        {
            var viewModel = new ItemEUViewModel
            {
                TenderGuid = tenderGuid,
                LotStringId = lotId
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addItemEU")]
        [HttpPost]
        public async Task<ActionResult> AddItemEU(ItemEUViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await TenderProvider.AddItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/editItemEU/{itemId}")]
        [HttpGet]
        public async Task<ActionResult> EditItemEU(Guid tenderGuid, string itemId)
        {
            var item = await TenderProvider.GetItem(tenderGuid, itemId);
            var viewModel = new ItemEUViewModel(tenderGuid, item);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editItemEU/{itemId}")]
        [HttpPost]
        public async Task<ActionResult> EditItemEU(ItemEUViewModel viewModel)
        {
            await CheckCPVGroupNumber(viewModel.TenderGuid, viewModel.Classification);

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var draftItemDTO = viewModel.ToDTO();
            await TenderProvider.EditItem(viewModel.TenderGuid, draftItemDTO);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        private async Task CheckCPVGroupNumber(Guid tenderGuid, ClassificationViewModel lot)
        {
            var cpvList = await TenderProvider.GetTenderCPVClassifications(tenderGuid);

            var lotCPV = lot.Id;
            int firstSymbol = 3;
            string lotGroup = lotCPV.Substring(0, firstSymbol);

            bool isTrue = cpvList.All(m => m.Substring(0, firstSymbol) == lotGroup);
           
            if (!isTrue)
            {
                string currentCPV = cpvList.FirstOrDefault().Substring(0, firstSymbol);
                ModelState.AddModelError("Classification.Id", $"Перші 3-и символи класифікатору повинні співпадати для всіх закупівель. Оберіть код який починається на '{currentCPV}', або змініть класифікатор для всіх об'єктів тендеру.");
            }
        }

    }
}