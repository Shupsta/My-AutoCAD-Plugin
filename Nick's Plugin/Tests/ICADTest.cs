using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tests
{
    public interface ICADTest
    {
        bool run();

        string getFailedTests();
    }
}
