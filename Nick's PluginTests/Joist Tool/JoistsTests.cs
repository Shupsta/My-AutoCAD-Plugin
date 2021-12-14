using Microsoft.VisualStudio.TestTools.UnitTesting;
using WBPlugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tests
{
    [TestClass()]
    public class JoistsTests
    {
        [TestMethod()]
        public void JoistsTest()
        {
            Joists testObj = new Joists();
            Assert.AreEqual(16, testObj.getSpacing());
        }
    }
}