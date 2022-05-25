using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace REPS.UI.Controllers
{
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            ViewData["SelectedLayout"] = "Admin";
            return View();
        }

        public ActionResult Users()
        {
            return View();
        }
    }
}