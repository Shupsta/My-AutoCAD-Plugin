using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nick_s_Plugin.InputRetrievers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_s_Plugin.InputRetrievers.Tests
{
    [TestClass()]
    public class DoubleInputRetrieverTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.IO.FileNotFoundException))]
        public void getUserInputTest()
        {
            IUserInputRetriever<Double> testObj = new DoubleInputRetriever();
            testObj.getUserInput("Test prompt", 16);
        }
    }
}