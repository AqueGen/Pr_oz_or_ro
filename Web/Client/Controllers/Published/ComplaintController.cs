using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Security;
using Kapitalist.Web.Client.ViewModels.Published;


namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class ComplaintController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }
        private IAccessManager AccessManager { get; set; }

        public ComplaintController(ITenderProvider tenderProvider, IAccessManager accessManager)
        {
            TenderProvider = tenderProvider;
            AccessManager = accessManager;
        }

        [Route("tender/{tenderGuid:guid}/addComplaint")]
        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addComplaint")]
        [HttpGet]
        [Authorize]
        public ActionResult AddComplaint(Guid tenderGuid, string lotId)
        {
            var viewModel = new TenderComplaintViewModel(tenderGuid, lotId);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addComplaint")]
        [Route("tender/{tenderGuid:guid}/lot/{lotId}/addComplaint")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComplaint(TenderComplaintViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var tenderComplaintDTO = viewModel.ToDTO();
            await TenderProvider.AddTenderComplaint(viewModel.TenderGuid, tenderComplaintDTO);
            return RedirectToAction("Info", "TenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/editComplaint/{complaintId}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> EditComplaint(Guid tenderGuid, string complaintId)
        {
            await TenderProvider.CheckComplaintAuthor(tenderGuid, complaintId);

            var complaintDTO = await TenderProvider.GetTenderComplaint(tenderGuid, complaintId);

            var viewModel = new TenderComplaintViewModel(tenderGuid, complaintDTO)
            {
                TenderGuid = tenderGuid
            };

            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/editComplaint/{complaintId}")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditComplaint(TenderComplaintViewModel viewModel)
        {
            await TenderProvider.CheckComplaintAuthor(viewModel.TenderGuid, viewModel.StringId);
            if (!ModelState.IsValid)
            {
                var lots = await TenderProvider.GetLots(viewModel.TenderGuid);
                return View(viewModel);
            }

            var tenderComplaintDTO = viewModel.ToDTO();
            await TenderProvider.EditTenderComplaint(tenderComplaintDTO);

            return RedirectToAction("Info", "TenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }

        [Route("tender/{tenderGuid:guid}/complaint/{complaintId}/addAnswer")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AddAnswer(Guid tenderGuid, string complaintId)
        {
            await TenderProvider.CheckTenderAuthor(tenderGuid);
            var viewModel = new TenderComplaintViewModel(tenderGuid);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/complaint/{complaintId}/addAnswer")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAnswer(TenderComplaintViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            await TenderProvider.CheckTenderAuthor(viewModel.TenderGuid);
            var complaintDTO = viewModel.ToDTO();
            await TenderProvider.AddComplaintAnswer(viewModel.TenderGuid, complaintDTO);
            return RedirectToAction("Info", "TenderInfo", new {tenderGuid = viewModel.TenderGuid});
        }


        
    }
}