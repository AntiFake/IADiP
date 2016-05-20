using System;
using System.IO;
using System.Linq;
using System.Text;
using IADIP.Classes;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IADIP.Helpers
{
    public static class FileHelper
    {
        public static void LoadGoogleCsvTestResults(Stream input, ref List<string> questions, ref List<List<string>> results)
        {
            using (var reader = new StreamReader(input, Encoding.UTF8))
            {
                string[] separator = new string[] { "\",\"" };
                questions = reader.ReadLine().Split(separator, StringSplitOptions.None).ToList();
                questions.RemoveAt(0);

                while (!reader.EndOfStream)
                {
                    List<string> answers = reader.ReadLine().Split(separator, StringSplitOptions.None).ToList();
                    answers.RemoveAt(0);
                    results.Add(answers);
                }
            }
        }

        /// <summary>
        /// Загружает результаты опроса из Google Forms.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="respondents"></param>
        /// <returns></returns>
        public static List<Respondent> LoadGoogleJson(Stream input)
        {
            List<Respondent> respondents = new List<Respondent>();
            string json = string.Empty;
            using (var reader = new StreamReader(input, Encoding.UTF8))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Respondent>>(json);
        }

        public static List<Respondent> LoadGoogleJson(string path)
        {
            string json = string.Empty;
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Respondent>>(json);
        }

        /// <summary>
        /// Загружает атрибуты из json-файла.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<Attr> LoadJsonAttributes(string path)
        {
            string json = string.Empty;
            using (var reader = new StreamReader(path))
            {
                json = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Attr>>(json);
        }

        public static List<Question> LoadSurveyFromJson(string path)
        {
            List<Question> survey = new List<Question>();

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                survey = JsonConvert.DeserializeObject<List<Question>>(json);
            }
            
            return survey;
        }
    }
}