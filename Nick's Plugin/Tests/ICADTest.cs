using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_s_Plugin.Tests
{
    interface ICADTest
    {
        bool run();

        string getFailedTests();
    }
}
