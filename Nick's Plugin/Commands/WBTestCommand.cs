using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Tests;

namespace WBPlugin
{
    public class WBTestCommand
    {
        public static string InputTests(ICADTest testObj)
        {

            if (testObj.run() == false)
            {
                StringBuilder testResults = new StringBuilder();
                testResults.AppendFormat("\n %s", testObj.getFailedTests());

                return testResults.ToString();
            }
            else
            {
                return "\n All tests passed!";
            }
        }
    }
}
