using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Consts;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Exceptions;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels;
using Kapitalist.Web.Client.ViewModels.Base;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{
    [Authorize]
    [RoutePrefix("draft")]
    public class DraftTenderInfoController : BaseDraftController
    {
        private IDraftProvider DraftProvider { get; }
        public IProfileProvider ProfileProvider { get;}

        public DraftTenderInfoController(IDraftProvider draftProvider, Lazy<IAccessManager> accessManager, IProfileProvider profileProvider)
            : base(accessManager)
        {
            DraftProvider = draftProvider;
            ProfileProvider = profileProvider;
        }


        [Route("tender/{tenderGuid:guid}/info")]
        [HttpGet]
        public async Task<ActionResult> Info(Guid tenderGuid)
        {
            var tender = await DraftProvider.GetDraftTender(tenderGuid);

            TenderValidation(tender);

            switch (tender.ProcurementMethodType)
            {
                case ProcurementMethodType.ABOVE_THRESHOLD_UA:
                {
                    var viewModel = new DraftTenderAboveThresholdUAViewModel(tender);
                    return View("InfoAboveThresholdUA", viewModel);
                }
                case ProcurementMethodType.ABOVE_THRESHOLD_EU:
                {
                    var viewModel = new DraftTenderAboveThresholdEUViewModel(tender);
                    return View("InfoAboveThresholdEU", viewModel);
                }
                case ProcurementMethodType.REPORTING:
                {
                    var viewModel = new DraftTenderLimitedReportingViewModel(tender);
                    return View("InfoTenderLimitedReporting", viewModel);
                }
                case ProcurementMethodType.NEGOTIATION:
                {
                    var viewModel = new DraftTenderLimitedViewModel(tender);
                    return View("InfoTenderLimited", viewModel);
                }
                case ProcurementMethodType.NEGOTIATION_QUICK:
                {
                    var viewModel = new DraftTenderLimitedQuickViewModel(tender);
                    return View("InfoTenderLimitedQuick", viewModel);
                }
                case ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE:
                {
                    var viewModel = new DraftTenderDefenseViewModel(tender);
                    return View("InfoTenderDefense", viewModel);
                }
                default:
                {
                    var viewModel = new DraftTenderBelowThresholdViewModel(tender);
                    return View("InfoBelowThreshold", viewModel);
                }
            }
            throw new BadRequestException();
        }

        [Route("tender/{tenderGuid:guid}/publish")]
        [HttpPost]
        public async Task<ActionResult> Publish(Guid tenderGuid)
        {
            var newTenderGuid = await DraftProvider.PublishDraftTender(tenderGuid);
            var autoRedirect = true;
            if (autoRedirect)
            {
                return RedirectToAction("Info", "TenderInfo", new {tenderGuid = newTenderGuid});
            }
            else
            {
                return RedirectToAction("TenderStatus", new {tenderGuid = newTenderGuid});
            }
        }

        [Route("tender/{tenderGuid:guid}/tenderStatus")]
        [HttpGet]
        public async Task<ActionResult> TenderStatus(Guid? tenderGuid)
        {
            return View(tenderGuid);
        }


        [Route("ajax/getCPV")]
        [HttpGet]
        public string GetCPV()
        {
            string response = null;
            var path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
                "Content/cpv/cpv_ukr_tree_hierarchy.json");
            if (System.IO.File.Exists(path))
            {
                response = System.IO.File.ReadAllText(path);
            }
            return response;
        }

        [Route("getGSIN")]
        [HttpGet]
        public string GetGSIN()
        {
            string response = null;
            var path = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
                "Content/gsin/gsin.json");
            if (System.IO.File.Exists(path))
            {
                response = System.IO.File.ReadAllText(path);
            }
            return response;
        }

        private async Task TenderValidation(DraftTenderDTO tender)
        {
            string type = tender.ProcurementMethodType;

            if (type == ProcurementMethodType.BELOW_THRESHOLD
                || type == ProcurementMethodType.ABOVE_THRESHOLD_UA
                || type == ProcurementMethodType.ABOVE_THRESHOLD_EU
                || type == ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE)
            {
                if (tender.Lots.Count == 0)
                {
                    ModelState.AddModelError("", "Потрібно додати хоча б один лот.");
                }
            }

            var items = new List<ItemDTO>();
            foreach (var lot in tender.Lots)
            {
                items.AddRange(lot.Items);
            }
            items.AddRange(tender.Items);
            if (items.Count == 0)
            {
                ModelState.AddModelError("", "Потрібно додати хоча б одну закупівлю.");
            }

            if (type == ProcurementMethodType.ABOVE_THRESHOLD_EU
                || type == ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE)
            {
                if (!tender.Contacts.Any())
                {
                    ModelState.AddModelError("", "Потрібно додати хоча б один контакт.");
                }
            }

            if (type == ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE)
            {
                int userId = AccessManager.Value.UserOrganizationId;
                var account = await ProfileProvider.GetUserOrganization(userId);

                if (account.Kind != ProcuringEntityType.Defense)
                {
                    ModelState.AddModelError("", "Аккаунт повинен бути Defense типу.");
                }
            }
        }


    }
}