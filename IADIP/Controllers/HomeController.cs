using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IADIP.Helpers;
using IADIP.Models;
using IADIP.Classes;
using System.IO;
using Newtonsoft.Json;

namespace IADIP.Controllers
{
    public class HomeController : Controller
    {
        public class Row
        {
            public double x;
            public double y;
            public double z;
            public string color;
        }

        public static double[][] RandomData(int d1, int d2)
        {
            double[][] data = new double[d1][];
            Random rnd = new Random();

            for (int i = 0; i < d1; i++)
            {
                data[i] = new double[d2];
                for (int j = 0; j < d2; j++)
                {
                    data[i][j] = rnd.NextDouble() * 10;
                }
            }

            return data;
        }

        public static List<Row> GetClusteredData(double[][] data, int[] clustering, string[] colors, int numClusters)
        {
            List<Row> list = new List<Row>();

            for (int k = 0; k < numClusters; ++k)
            {
                for (int i = 0; i < data.Length; ++i)
                {
                    int clusterID = clustering[i];
                    if (clusterID != k) continue;
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        list.Add(new Row()
                        {
                            color = colors[clusterID],
                            x = data[i][0],
                            y = data[i][1],
                            z = data[i][2]
                        });
                        //if (data[i][j] >= 0.0) Console.Write(" ");
                        //Console.Write(data[i][j].ToString("F" + decimals) + " ");
                    }
                }
            }

            return list;
        }

        // GET: Home
        public ActionResult Index()
        {
            int numClusters = 3;

            double[][] data = RandomData(100, 3);
            int[] clusters = ClusterAnalysis.Cluster(data, numClusters);

            ViewBag.data = JsonConvert.SerializeObject(GetClusteredData(data, clusters, new string[] { "red", "blue", "green" }, numClusters));

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