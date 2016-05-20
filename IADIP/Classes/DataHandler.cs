using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Classes
{
    public static class DataHandler
    {
        /// <summary>
        /// Сопоставление ответов на вопросы и весов.
        /// </summary>
        /// <param name="respondents"></param>
        /// <returns></returns>
        public static List<Respondent> HandleInitialData(List<Respondent> respondents)
        {
            foreach (var respondent in respondents)
            {
                // i - номер вопроса.
                // respondent.Answers[i] - номер ответа.
                respondent.AnswerWeights = new double[respondent.Answers.Length];

                for (int i = 0; i < respondent.Answers.Length; i++)
                {
                    switch (respondent.Type)
                    {
                        case "J":
                            respondent.AnswerWeights[i] = GlobalVariables.Survey[i].Options[respondent.Answers[i]].oWeights[0];
                            break;
                        case "M":
                            respondent.AnswerWeights[i] = GlobalVariables.Survey[i].Options[respondent.Answers[i]].oWeights[1];
                            break;
                        case "S":
                            respondent.AnswerWeights[i] = GlobalVariables.Survey[i].Options[respondent.Answers[i]].oWeights[2];
                            break;
                    }
                }
            }
            return respondents;
        }

        /// <summary>
        /// Считает значения атрибутов для каждого разработчика.
        /// </summary>
        /// <returns></returns>
        public static List<Respondent> CalculateAttributes(List<Respondent> respondents)
        {
            foreach (var respondent in respondents)
            {
                respondent.Attributes = new double[GlobalVariables.Attributes.Count];

                for (int i = 0; i < GlobalVariables.Attributes.Count; i++)
                {
                    // i - номер вопроса.
                    double sum = 0.0; // сумма весов по вопросам.
                    for (int j = 0; j < GlobalVariables.Attributes[i].Questions.Length; j++)
                    {
                        sum += (GlobalVariables.Attributes[i].Beta[j] * respondent.AnswerWeights[j] * j / (GlobalVariables.Survey[j].Options.Count - 1));
                    }
                    //sum /= GlobalVariables.Attributes[i].Questions.Length;
                    
                    // Сохраняем вес атрибута для респондента.
                    respondent.Attributes[i] = sum;
                }
            }

            return respondents;
        }
    }
}