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

            //return View("TestResults", new TestResultsViewModel(GlobalVariables.Questions, GlobalVariables.InitialResults));
            return RedirectToAction("Report");
        }

        public ActionResult DecisionTree()
        {
            return View();
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
            List<DataRow> result = ClusterAnalysis.GetClusteredData(data, clusters, new string[] { "blue", "red", "green" }, numClusters, index);
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
            ViewBag.office_1g = JsonConvert.SerializeObject(result.Where(i => i.office == "1"));
            ViewBag.office_2g = JsonConvert.SerializeObject(result.Where(i => i.office == "2"));

            // Компания 2.
            ViewBag.office_3g = JsonConvert.SerializeObject(result.Where(i => i.office == "3"));
            ViewBag.office_4g = JsonConvert.SerializeObject(result.Where(i => i.office == "4"));
            ViewBag.office_5g = JsonConvert.SerializeObject(result.Where(i => i.office == "5"));

            // Темпоральная характеристика.
            // Juniors.
            List<DataPoint> junior = new List<DataPoint>()
            {
                new DataPoint()
                {
                    month = 1,
                    sum = 3.48
                },
                new DataPoint()
                {
                    month = 2,
                    sum = 3.81
                },
                new DataPoint()
                {
                    month = 3,
                    sum = 3.3
                },
            };
            ViewBag.juniorTotalGrowth = JsonConvert.SerializeObject(junior);

            // Middles.
            List<DataPoint> middle = new List<DataPoint>()
            {
                new DataPoint()
                {
                    month = 1,
                    sum = 7.19
                },
                new DataPoint()
                {
                    month = 2,
                    sum = 7.95
                },
                new DataPoint()
                {
                    month = 3,
                    sum = 7.4
                },
            };
            ViewBag.middleTotalGrowth = JsonConvert.SerializeObject(middle);

            // Seniors.
            List<DataPoint> senior = new List<DataPoint>()
            {
                new DataPoint()
                {
                    month = 1,
                    sum = 10.65
                },
                new DataPoint()
                {
                    month = 2,
                    sum = 10.99
                },
                new DataPoint()
                {
                    month = 3,
                    sum = 8.87
                },
            };
            ViewBag.seniorTotalGrowth = JsonConvert.SerializeObject(senior);

            //ViewBag.juniorTotalGrowth = JsonConvert.SerializeObject
            //    (
            //        GetTemporalCharacteristics
            //        (
            //            new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
            //            "J",
            //            index
            //        )
            //    );
            //ViewBag.middleTotalGrowth = JsonConvert.SerializeObject
            //    (
            //        GetTemporalCharacteristics
            //        (
            //            new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
            //            "M",
            //            index

            //        )
            //    );
            //ViewBag.seniorTotalGrowth = JsonConvert.SerializeObject
            //    (
            //        GetTemporalCharacteristics
            //        (
            //            new string[] { AppDomain.CurrentDomain.BaseDirectory + "Files/res1.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res2.json", AppDomain.CurrentDomain.BaseDirectory + "Files/res3.json" },
            //            "S",
            //            index
            //        )
            //    );


            // Резюмирование
            #region Общее
            var juniors = result.Where(i => i.color == "green").ToList();
            var middles = result.Where(i => i.color == "blue").ToList();
            var seniors = result.Where(i => i.color == "red").ToList();

            ViewBag.juniors = juniors;
            ViewBag.middles = middles;
            ViewBag.seniors = seniors;

            ViewBag.juniorCount = juniors.Count;
            ViewBag.middleCount = middles.Count;
            ViewBag.seniorCount = seniors.Count;
            ViewBag.totalCount = result.Count();

            //Джуниоры
            ViewBag.juniorMinExperience = juniors.Select(i => i.x).Min();
            ViewBag.juniorAvgExperience = juniors.Select(i => i.x).Average();
            ViewBag.juniorMaxExperience = juniors.Select(i => i.x).Max();

            ViewBag.juniorMinPerformance = juniors.Select(i => i.y).Min();
            ViewBag.juniorAvgPerformance = juniors.Select(i => i.y).Average();
            ViewBag.juniorMaxPerformance = juniors.Select(i => i.y).Max();

            ViewBag.juniorMinPos = juniors.Select(i => i.z).Min();
            ViewBag.juniorAvgPos = juniors.Select(i => i.z).Average();
            ViewBag.juniorMaxPos = juniors.Select(i => i.z).Max();

            var topJunior = FindMaxExtremum(juniors);
            var juniorCompetence = CalculateCompetence(topJunior, juniors);
            ViewBag.topJunior = topJunior;
            ViewBag.juniorCompetence = juniorCompetence;

            //Мидлы
            ViewBag.middleMinExperience = middles.Select(i => i.x).Min();
            ViewBag.middleAvgExperience = middles.Select(i => i.x).Average();
            ViewBag.middleMaxExperience = middles.Select(i => i.x).Max();

            ViewBag.middleMinPerformance = middles.Select(i => i.y).Min();
            ViewBag.middleAvgPerformance = middles.Select(i => i.y).Average();
            ViewBag.middleMaxPerformance = middles.Select(i => i.y).Max();

            ViewBag.middleMinPos = middles.Select(i => i.z).Min();
            ViewBag.middleAvgPos = middles.Select(i => i.z).Average();
            ViewBag.middleMaxPos = middles.Select(i => i.z).Max();

            var topMiddle = FindMaxExtremum(middles);
            var middleCompetence = CalculateCompetence(topMiddle, middles);
            ViewBag.topMiddle = topMiddle;
            ViewBag.middleCompetence = middleCompetence;

            // Сеньоры
            ViewBag.seniorMinExperience = seniors.Select(i => i.x).Min();
            ViewBag.seniorAvgExperience = seniors.Select(i => i.x).Average();
            ViewBag.seniorMaxExperience = seniors.Select(i => i.x).Max();

            ViewBag.seniorMinPerformance = seniors.Select(i => i.y).Min();
            ViewBag.seniorAvgPerformance = seniors.Select(i => i.y).Average();
            ViewBag.seniorMaxPerformance = seniors.Select(i => i.y).Max();

            ViewBag.seniorMinPos = seniors.Select(i => i.z).Min();
            ViewBag.seniorAvgPos = seniors.Select(i => i.z).Average();
            ViewBag.seniorMaxPos = seniors.Select(i => i.z).Max();

            var topSenior = FindMaxExtremum(seniors);
            var seniorCompetence = CalculateCompetence(topSenior, seniors);
            ViewBag.topSenior = topSenior;
            ViewBag.seniorCompetence = seniorCompetence;
            #endregion

            #region Пространственная характеристика

            // Компания 1.
            var office_1 = GetEmployeesDistribution(result.Where(i => i.office == "1").ToList());
            ViewBag.office_1 = office_1;

            var office_2 = GetEmployeesDistribution(result.Where(i => i.office == "2").ToList());
            ViewBag.office_2 = office_2;

            // Компания 2.
            var office_3 = GetEmployeesDistribution(result.Where(i => i.office == "3").ToList());
            ViewBag.office_3 = office_3;

            var office_4 = GetEmployeesDistribution(result.Where(i => i.office == "4").ToList());
            ViewBag.office_4 = office_4;

            var office_5 = GetEmployeesDistribution(result.Where(i => i.office == "5").ToList());
            ViewBag.office_5 = office_5;
            #endregion

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

                string[] office = new string[] { "3", "4", "5" };

                var programmers = respondents.Where(i => i.Type == category && office.Contains(i.Office)).ToList();

                for (int i = 0; i < programmers.Count; i++)
                {
                    sum += programmers[i].Attributes[0];
                    sum += programmers[i].Attributes[1];
                    sum += programmers[i].Attributes[2];
                }

                points.Add(new DataPoint() { month = j + 1, sum = (sum * 15) / programmers.Count });
            }

            return points;
        }

        #region Резюмирование

        #region Общее

        // Поиск локального экстремума
        private DataRow FindMaxExtremum(List<DataRow> result)
        {
            DataRow max = result[0];

            for (int i = 1; i < result.Count; i++)
            {
                if (max.Magnitude < result[i].Magnitude)
                    max = result[i];
            }

            return max;
        }

        // Расчет компетенции сотрудника.
        private List<ResumeRaw> CalculateCompetence(DataRow extremum, List<DataRow> result)
        {
            List<ResumeRaw> competence = new List<ResumeRaw>();
            double value;
            ResumeRaw rawData = null;

            for (int i = 0; i < result.Count; i++)
            {
                value = Math.Round((Math.Abs(extremum.Magnitude - result[i].Magnitude) / extremum.Magnitude) * 100, 2);

                // Расчет по таблице с компетенциями.
                if (value >= 50)
                    rawData = new ResumeRaw()
                    {
                        text = "Низкий уровень компетенции",
                        value = value,
                        number = i + 1,
                        className = "red-tr"
                    };
                if (value >= 30 && value < 50)
                    rawData = new ResumeRaw()
                    {
                        text = "Средний уровень компетенции",
                        value = value,
                        number = i + 1,
                        className = "blue-tr"
                    };
                if (value < 30)
                    rawData = new ResumeRaw()
                    {
                        text = "Высокий уровень компетенции",
                        value = value,
                        number = i + 1,
                        className = "green-tr"
                    };

                competence.Add(rawData);
            }

            return competence;
        }
        #endregion

        #region Пространственная

        private List<ResumeRaw> GetEmployeesDistribution(List<DataRow> result)
        {
            var totalCount = result.Count;
            var juniorCount = result.Count(i => i.type == "J");
            var middleCount = result.Count(i => i.type == "M");
            var seniorCount = result.Count(i => i.type == "S");

            double juniorCoeff = Math.Round((double)juniorCount / totalCount * 100, 2);
            double middleCoeff = Math.Round((double)middleCount / totalCount * 100, 2);
            double seniorCoeff = Math.Round((double)seniorCount / totalCount * 100, 2);

            List<ResumeRaw> resume = new List<ResumeRaw>();
            ResumeRaw rawData = null;

            // Junior
            //N >= 35 % -неблагоприятное распределение;
            //20 % <= N < 35 % -терпимое распределение;
            //N < 20 % -благоприятное распределение.
            if (juniorCoeff >= 35)
                rawData = new ResumeRaw()
                {
                    className = "red-tr",
                    valueName = "Junior",
                    text = "Неблагоприятное распределение",
                    value = juniorCoeff,
                    number = 1
                };

            if (juniorCoeff < 35 && juniorCoeff >= 20)
                rawData = new ResumeRaw()
                {
                    className = "blue-tr",
                    valueName = "Junior",
                    text = "Терпимое распределение",
                    value = juniorCoeff,
                    number = 1
                };

            if (juniorCoeff < 20)
                rawData = new ResumeRaw()
                {
                    className = "green-tr",
                    valueName = "Junior",
                    text = "Благоприятное распределение",
                    value = juniorCoeff,
                    number = 1
                };

            resume.Add(rawData);

            // Middle
            //N >= 60 % -неблагоприятное распределение;
            //40 % <= N < 60 % -терпимое распределение;
            //N < 40 % -благоприятное распределение.
            if (middleCoeff >= 60)
                rawData = new ResumeRaw()
                {
                    className = "red-tr",
                    valueName = "Middle",
                    text = "Неблагоприятное распределение",
                    value = middleCoeff,
                    number = 2
                };

            if (middleCoeff < 60 && middleCoeff >= 40)
                rawData = new ResumeRaw()
                {
                    className = "blue-tr",
                    valueName = "Middle",
                    text = "Терпимое распределение",
                    value = middleCoeff,
                    number = 2
                };

            if (middleCoeff < 40)
                rawData = new ResumeRaw()
                {
                    className = "green-tr",
                    valueName = "Middle",
                    text = "Благоприятное распределение",
                    value = middleCoeff,
                    number = 2
                };
            resume.Add(rawData);

            // Seniors
            //N >= 20 % -неблагоприятное распределение;
            //10 % <= N < 20 % -терпимое распределение;
            //N < 10 % -благоприятное распределение.
            if (seniorCoeff >= 20)
                rawData = new ResumeRaw()
                {
                    className = "red-tr",
                    valueName = "Senior",
                    text = "Неблагоприятное распределение",
                    value = seniorCoeff,
                    number = 3
                };

            if (seniorCoeff < 20 && seniorCoeff >= 10)
                rawData = new ResumeRaw()
                {
                    className = "blue-tr",
                    valueName = "Senior",
                    text = "Терпимое распределение",
                    value = seniorCoeff,
                    number = 3
                };

            if (seniorCoeff < 10)
                rawData = new ResumeRaw()
                {
                    className = "green-tr",
                    valueName = "Senior",
                    text = "Благоприятное распределение",
                    value = seniorCoeff,
                    number = 3
                };

            resume.Add(rawData);

            return resume;
        }
        #endregion
        #endregion

    }
}