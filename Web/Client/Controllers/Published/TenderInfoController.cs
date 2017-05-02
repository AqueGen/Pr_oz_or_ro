using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models.Consts;
using Kapitalist.Services.Prozorro.Exceptions;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.ViewModels.Drafts;
using Kapitalist.Web.Client.ViewModels.Published;


namespace Kapitalist.Web.Client.Controllers.Published
{
    public class TenderInfoController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public TenderInfoController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        //[Route("tender/{tenderGuid:guid}/details")]
        //[HttpGet]
        //public async Task<ActionResult> Details(Guid tenderGuid)
        //{
        //    var tender = await TenderProvider.GetTender(tenderGuid);

        //    DetailsViewModel viewModel = new DetailsViewModel(tender);
        //    return View(viewModel);
        //}

        [Route("tender/{tenderGuid:guid}/info")]
        [HttpGet]
        public async Task<ActionResult> Info(Guid tenderGuid)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);

            switch (tender.ProcurementMethodType)
            {
                case ProcurementMethodType.ABOVE_THRESHOLD_UA:
                {
                    var viewModel = new TenderAboveThresholdUAViewModel(tender);
                    return View("InfoAboveThresholdUA", viewModel);
                }
                case ProcurementMethodType.ABOVE_THRESHOLD_EU:
                {
                    var viewModel = new TenderAboveThresholdEUViewModel(tender);
                    return View("InfoAboveThresholdEU", viewModel);
                }
                case ProcurementMethodType.REPORTING:
                {
                    var viewModel = new TenderLimitedReportingViewModel(tender);
                    return View("InfoTenderLimitedReporting", viewModel);
                }
                case ProcurementMethodType.NEGOTIATION:
                {
                    var viewModel = new TenderLimitedViewModel(tender);
                    return View("InfoTenderLimited", viewModel);
                }
                case ProcurementMethodType.NEGOTIATION_QUICK:
                {
                    var viewModel = new TenderLimitedQuickViewModel(tender);
                    return View("InfoTenderLimitedQuick", viewModel);
                }
                case ProcurementMethodType.ABOVE_THRESHOLD_UA_DEFENSE:
                {
                    var viewModel = new TenderDefenseViewModel(tender);
                    return View("InfoTenderDefense", viewModel);
                }
                default:
                {
                    var viewModel = new TenderBelowThresholdViewModel(tender);
                    return View("InfoBelowThreshold", viewModel);
                }
            }
            throw new BadRequestException();
        }
    }
}