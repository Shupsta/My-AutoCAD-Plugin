using Microsoft.VisualStudio.TestTools.UnitTesting;
using WBPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPluginTests.Fakes;

namespace WBPlugin.Tests
{
    [TestClass()]
    public class WBCommandsTests
    {
        [TestMethod()]
        public void JoistLayerCommandTest()
        {
            try
            {
                WBCommands.JoistLayer();
            }
            catch
            {
                Assert.Fail();
            }
            
        }

        [TestMethod()]
        public void InputTestCommandTest()
        {
            
            WBTestCommand.InputTests(new fakeDoubleInputRetriverTest());

        }
    }
}