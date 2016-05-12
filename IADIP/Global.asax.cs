using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IADIP.Classes;
using IADIP.Helpers;

namespace IADIP
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // ...
            GlobalVariables.Init();
            GlobalVariables.Survey = FileHelper.LoadSurveyFromJson(Server.MapPath("Files/survey.json"));
        }
    }
}
