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
        public static List<string> Questions;
        public static List<List<string>> InitialResults;
        public static List<Question> Survey;
        public static List<Respondent> Respondents;
        public static List<Attr> Attributes;

        static GlobalVariables()
        {
            Questions = new List<string>();
            InitialResults = new List<List<string>>();
            Survey = new List<Question>();
            Respondents = new List<Respondent>();
        }

        public static void Init()
        {
            Questions.Clear();
            InitialResults.Clear();
            Respondents.Clear();
        }
    }


}