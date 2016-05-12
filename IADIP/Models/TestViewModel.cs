using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IADIP.Classes;

namespace IADIP.Models
{
    public class TestViewModel
    {
        public List<Question> Survey;

        public TestViewModel(List<Question> survey)
        {
            Survey = survey;
        }
    }
}