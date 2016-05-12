using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Classes
{
    /// <summary>
    /// Класс для хранения глобальных переменных.
    /// </summary>
    public static class GlobalVariables
    {
        public static List<string> TestHeaders;
        public static List<List<string>> TestResults;
        public static List<Question> Survey;

        static GlobalVariables()
        {
            TestHeaders = new List<string>();
            TestResults = new List<List<string>>();
            Survey = new List<Question>();
        }

        public static void Init()
        {
            TestHeaders.Clear();
            TestResults.Clear();
        }
    }


}