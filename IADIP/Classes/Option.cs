using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Classes
{
    public class Option
    {
        public string Text { get; set; }
        /// <summary>
        /// Данный список состоит из трех элементов: 0 - J, 1 - M, 2 - S 
        /// </summary>
        public List<double> Weights { get; set; }
    }
}