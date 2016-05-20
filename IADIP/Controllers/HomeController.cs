using System.Web;
using System.Web.Mvc;
using IADIP.Helpers;
using IADIP.Models;
using IADIP.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace IADIP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //int numClusters = 3;
            //double[][] data = ClusterAnalysis.RandomData(100, 3);
            //int[] clusters = ClusterAnalysis.Cluster(data, numClusters);
            //ViewBag.data = JsonConvert.SerializeObject(ClusterAnalysis.GetClusteredData(data, clusters, new string[] { "red", "blue", "green" }, numClusters));

            return View();
        }

        public ActionResult Test()
        {
            return View(new TestViewModel(GlobalVariables.Survey));
        }

        public ActionResult TestResults()
        {
            return View(new TestResultsViewModel(GlobalVariables.Questions, GlobalVariables.InitialResults));
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
                GlobalVariables.Respondents = FileHelper.LoadGoogleJson(file.InputStream);

            GlobalVariables.Respondents = DataHandler.HandleInitialData(GlobalVariables.Respondents);
            GlobalVariables.Attributes = FileHelper.LoadJsonAttributes(AppDomain.CurrentDomain.BaseDirectory + @"Files/attributes.json");

            GlobalVariables.Respondents = DataHandler.CalculateAttributes(GlobalVariables.Respondents);

            return View("TestResults", new TestResultsViewModel(GlobalVariables.Questions, GlobalVariables.InitialResults));
        }

        public ActionResult Report()
        {
            int index = 35;

            // Общий кластерный анализ.
            int numClusters = 3;
            double[][] data = new double[GlobalVariables.Respondents.Count][];
            for (int i = 0; i < GlobalVariables.Respondents.Count; i++)
            {
                data[i] = GlobalVariables.Respondents[i].Attributes;
            }

            int[] clusters = ClusterAnalysis.Cluster(data, numClusters);
            List<DataRow> result = ClusterAnalysis.GetClusteredData(data, clusters, new string[] { "red", "blue", "green" }, numClusters, index);

            ViewBag.attrClusteredData = JsonConvert.SerializeObject(result);

            // Получение значений экспертной оценки для атрибутов.
            List<DataRow> expertAttrAssessment = new List<DataRow>(); // 3 = JMS.
            for (int i = 0; i < 3; i++)
            {
                expertAttrAssessment.Add(new DataRow());
                for (int j = 0; j < GlobalVariables.Attributes.Count; j++)
                {
                    expertAttrAssessment[i].x = GlobalVariables.Attributes[j].expertAssessment[i] * index;
                    expertAttrAssessment[i].y = GlobalVariables.Attributes[j].expertAssessment[i] * index;
                    expertAttrAssessment[i].z = GlobalVariables.Attributes[j].expertAssessment[i] * index;
                    expertAttrAssessment[i].color = "black";
                }
            }

            ViewBag.expertAttrAssessment = JsonConvert.SerializeObject(expertAttrAssessment);

            // Пространственная характеристика.
            // Компания 1.
            ViewBag.office_1 = JsonConvert.SerializeObject(result.Where(i => i.office == "1"));
            ViewBag.office_2 = JsonConvert.SerializeObject(result.Where(i => i.office == "2"));

            // Компания 2.
            ViewBag.office_3 = JsonConvert.SerializeObject(result.Where(i => i.office == "3"));
            ViewBag.office_4 = JsonConvert.SerializeObject(result.Where(i => i.office == "4"));
            ViewBag.office_5 = JsonConvert.SerializeObject(result.Where(i => i.office == "5"));

            // Темпоральная характеристика.
            ViewBag.juniorTotalGrowth = JsonConvert.SerializeObject
                (
                    GetTemporalCharacteristics
                    (
                        new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
                        "J",
                        index
                    )
                );
            ViewBag.middleTotalGrowth = JsonConvert.SerializeObject
                (
                    GetTemporalCharacteristics
                    (
                        new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
                        "M",
                        index

                    )
                );
            ViewBag.seniorTotalGrowth = JsonConvert.SerializeObject
                (
                    GetTemporalCharacteristics
                    (
                        new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
                        "S",
                        index
                    )
                );

            return View();
        }

        private List<DataPoint> GetTemporalCharacteristics(string[] paths, string category, int index)
        {
            List<Respondent> respondents = new List<Respondent>();
            List<DataPoint> points = new List<DataPoint>();

            for (int j = 0; j < paths.Length; j++)
            {
                respondents = FileHelper.LoadGoogleJson(paths[j]);
                respondents = DataHandler.HandleInitialData(respondents);
                respondents = DataHandler.CalculateAttributes(respondents);

                double sum = 0.0;

                var programmers = respondents.Where(i => i.Type == category).ToList();

                for (int i = 0; i < programmers.Count; i++)
                {
                    sum += programmers[i].Attributes[0];
                    sum += programmers[i].Attributes[1];
                    sum += programmers[i].Attributes[2];
                }

                points.Add(new DataPoint() { month = j + 1, sum = sum * index });
            }

            return points;
        }
    }
}