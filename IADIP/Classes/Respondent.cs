using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Classes
{
    public class Respondent
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Office { get; set; }
        public int[] Answers { get; set; }
        public double[] AnswerWeights { get; set; }
        public double[] Attributes { get; set; }
    }
}