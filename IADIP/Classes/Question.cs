using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Classes
{
    public class Question
    {
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }
}