using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Services.Prozorro.Providers;
using Kapitalist.Web.Security;
using Kapitalist.Web.Client.ViewModels.Published;
using Kapitalist.Web.Security.Helpers;

namespace Kapitalist.Web.Client.Controllers.Published
{
    [Authorize]
    public class QuestionController : BasePublishedController
    {
        private ITenderProvider TenderProvider { get; }

        public QuestionController(ITenderProvider tenderProvider)
        {
            TenderProvider = tenderProvider;
        }

        [Route("tender/{tenderGuid:guid}/question/{questionId}/addAnswer")]
        [HttpGet]
        public async Task<ActionResult> AddAnswer(Guid tenderGuid, string questionId)
        {
            if (questionId == tenderGuid.ToString("N"))
            {
                questionId = null;
            }
            var questionDTO = await TenderProvider.GetQuestion(tenderGuid, questionId);
            QuestionViewModel viewModel = new QuestionViewModel(tenderGuid, questionDTO);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/question/{questionId}/addAnswer")]
        [HttpPost]
        public async Task<ActionResult> AddAnswer(QuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var question = await TenderProvider.GetQuestion(viewModel.TenderGuid, viewModel.StringId);
            question.Answer = viewModel.Answer;

            await TenderProvider.AddAnswer(viewModel.TenderGuid, question);
            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }

        [Route("tender/{tenderGuid:guid}/addQuestion/{relatedTo}/{relatedId}")]
        [HttpGet]
        public async Task<ActionResult> AddQuestion(Guid tenderGuid, RelatedTo relatedTo, string relatedId)
        {
            var tender = await TenderProvider.GetTender(tenderGuid);
            QuestionViewModel viewModel = new QuestionViewModel(tenderGuid);
            return View(viewModel);
        }

        [Route("tender/{tenderGuid:guid}/addQuestion/{relatedTo}/{relatedId}")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> AddQuestion(QuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);
           
            var questionDTO = viewModel.ToDTO();

            var userId = User.GetUserOrganizationId();
            var author = await TenderProvider.GetContactPoint(userId);
            //questionDTO.Author = author; //TODO разобратся с ДТО
            await TenderProvider.AddQuestion(viewModel.TenderGuid, questionDTO);

            return RedirectToAction("Info", "TenderInfo", new { tenderGuid = viewModel.TenderGuid });
        }
    }
}