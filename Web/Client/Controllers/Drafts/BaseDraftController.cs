using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Kapitalist.Data.Models.DTO;
using Kapitalist.Services.Prozorro.Exceptions;
using Kapitalist.Web.Client.Controllers.Base;
using Kapitalist.Web.Security;

namespace Kapitalist.Web.Client.Controllers.Drafts
{

    public class BaseDraftController : BaseController
    {
        public Lazy<IAccessManager> AccessManager { get; set; }

        public BaseDraftController()
        {
        }

        public BaseDraftController(Lazy<IAccessManager> accessManager)
        {
            AccessManager = accessManager;
        }

    }
}