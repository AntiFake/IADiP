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
        public static void LoadGoogleCsvTestResults(Stream input, ref List<string> header, ref List<List<string>> results)
        {
            using (var reader = new StreamReader(input, Encoding.UTF8))
            {
                string[] separator = new string[] { "\",\"" };
                header = reader.ReadLine().Split(separator, StringSplitOptions.None).ToList();
                header.RemoveAt(0);

                while (!reader.EndOfStream)
                {
                    List<string> answers = reader.ReadLine().Split(separator, StringSplitOptions.None).ToList();
                    answers.RemoveAt(0);
                    results.Add(answers);
                }
            }
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