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
    public class JoistsTests
    {
        [TestMethod()]
        public void JoistsTestDefaultSpacing()
        {
            Joists testObj = new Joists();
            Assert.AreEqual(16, testObj.JoistSpacing);
        }

        [TestMethod()]
        public void NewSpacingTest()
        {
            DoubleInputRetriever fakeSpacing = new FakeDoubleInputRetriever(24);
            PointInputRetriever fakeStart = new FakePointInputRetriever(1, 1);
            AngleInputRetriever fakeAngle = new FakeAngleInputRetriever(90);
            BoundaryInputRetriever fakeBoundary = new FakeBoundaryInputRetriever();
            Joists testObj = new Joists(fakeSpacing, fakeStart, fakeAngle, fakeBoundary);
            Assert.AreEqual(24, testObj.JoistSpacing);
        }
    }
}