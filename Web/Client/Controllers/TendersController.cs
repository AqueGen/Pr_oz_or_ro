using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Data.Models.DTO.QueryDTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Client.ViewModels.Tenders;
using PagedList;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("tenders")]
    public class TendersController : BaseController
    {
        private Lazy<ITenderProvider> TenderProvider { get; }

        public TendersController(Lazy<ITenderProvider> tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("getTemplate/{template}")]
        [HttpGet]
        public PartialViewResult GetTemplate(string template)
        {
            return PartialView($"Searches/QueryComponents/{template}Partial");
        }

        [Route]
        [HttpGet]
        public async Task<ActionResult> Index(TenderQueryViewModel viewModel)
        {
            if (viewModel.PageNumber < 1)
                viewModel.PageNumber = 1;
            if (viewModel.PageSize < 1)
                viewModel.PageSize = 10;

            TenderQueryDTO filter = new TenderQueryDTO
            {
                Keyword = viewModel.Keyword,
                CpvCode = viewModel.CpvCode,
                ScgsCode = viewModel.GsinCode,
                ProcurementNumber = viewModel.ProcurementNumber,
                Procurer = viewModel.Procurer,
                Region = viewModel.Region,
                Status = viewModel.Status,
                ApplicationsSubmissionPeriod = viewModel.ApplicationsSubmissionPeriod?.ToDTO(),
                ClarificationPeriod = viewModel.ClarificationPeriod?.ToDTO(),
                AuctionPeriod = viewModel.AuctionPeriod?.ToDTO(),
                QualificationPeriod = viewModel.QualificationPeriod?.ToDTO(),
            };

            IPagedList<TenderDTO> tendersPagedList = await TenderProvider.Value.GetTendersPage(filter, viewModel.PageNumber, viewModel.PageSize);

            TendersViewModel tendersViewModel = new TendersViewModel(tendersPagedList, viewModel);

            if (Request.IsAjaxRequest())
            {
                return PartialView("Searches/Tenders/TenderTable", tendersViewModel);
            }
            else
            {
                return View("Tenders", tendersViewModel);
            }
        }
    }
}