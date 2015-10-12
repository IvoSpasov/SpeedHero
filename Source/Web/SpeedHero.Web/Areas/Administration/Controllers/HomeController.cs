using SpeedHero.Web.Areas.Administration.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedHero.Web.Areas.Administration.Controllers
{
    public class HomeController : AdminController
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}