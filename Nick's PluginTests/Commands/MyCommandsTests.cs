using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nick_s_Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nick_s_Plugin.Tests
{
    [TestClass()]
    public class MyCommandsTests
    {
        [TestMethod()]
        public void JoistLayerTest()
        {
            try
            {
                MyCommands.JoistLayer();
            }
            catch
            {
                Assert.Fail();
            }
            
        }
    }
}