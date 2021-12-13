using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nick_s_Plugin.InputRetrievers;

namespace Nick_s_Plugin.Tests
{
    class CADDoubleInputRetrieverTest : ICADTest
    {
        private string _failedTests;

        public CADDoubleInputRetrieverTest()
        {
            _failedTests = "";
        }

        public string getFailedTests()
        {
            return _failedTests;
        }

        public bool run()
        {
            bool allPassed = true;
            allPassed = BasicDoubleInputTest();

            return allPassed;
        }

        private bool BasicDoubleInputTest()
        {
            IUserInputRetriever<Double> testObj = new DoubleInputRetriever();
            double expected = 16;
            double actual = testObj.getUserInput("Should be 16", expected);

            if(actual != expected)
            {
                StringBuilder response = new StringBuilder();
                response.Append("\nBasicDoubleInputTest Failed with actual value =");
                response.Append(actual);
                _failedTests = response.ToString();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
