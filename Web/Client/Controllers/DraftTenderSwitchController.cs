using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kapitalist.Data.Models.Enums;
using Kapitalist.Web.Client.Controllers.Drafts;
using Kapitalist.Web.Client.ViewModels;

namespace Kapitalist.Web.Client.Controllers
{
    [RoutePrefix("draft")]
    [Authorize]
    public class DraftTenderSwitchController : BaseDraftController
    {
        [Route("type")]
        [HttpGet]
        public ActionResult Type()
        {
            //ProcuringEntityType companyType = //TODO тип зарегистрированный 

            return View();
        }
    }
}