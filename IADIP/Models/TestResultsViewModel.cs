using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IADIP.Models
{
    public class TestResultsViewModel
    {
        public List<string> TestHeaders;
        public List<List<string>> TestResults;

        public TestResultsViewModel(List<string> testHeader, List<List<string>> testResults)
        {
            TestHeaders = testHeader;
            TestResults = testResults;
        }
    }
}