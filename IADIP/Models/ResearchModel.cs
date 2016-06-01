using IADIP.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Models
{
    public class ResearchViewModel
    {
        public List<ResearchResultRow> ResearchResult;

        public ResearchViewModel(List<ResearchResultRow> rr)
        {
            ResearchResult = rr;
        }
    }
}