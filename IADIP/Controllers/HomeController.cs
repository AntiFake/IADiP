using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IADIP.Helpers;
using IADIP.Models;
using IADIP.Classes;
using System.IO;

namespace IADIP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View(new TestViewModel(GlobalVariables.Survey));
        }

        public ActionResult TestResults()
        {
            return View(new TestResultsViewModel(GlobalVariables.TestHeaders, GlobalVariables.TestResults));
        }

        public ActionResult LoadTestResults()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadTestResults(HttpPostedFileBase file)
        {
            GlobalVariables.Init();

            if (file.ContentLength > 0)
                FileHelper.LoadGoogleCsvTestResults(file.InputStream, ref GlobalVariables.TestHeaders, ref GlobalVariables.TestResults);

            return View("TestResults", new TestResultsViewModel(GlobalVariables.TestHeaders, GlobalVariables.TestResults));
        }
    }
}