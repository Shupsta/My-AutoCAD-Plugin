using NUnit.Framework;
using Moq;
using WBPlugin;
using WBPlugin.Zone_Tools;

namespace WBPlugin_Tests
{
    public class ZoneTests
    {
        

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public void Test1()
        {
            string zoneNum = "1A";
            Zone testZone = new Zone(new WBObjectId(123), zoneNum);
            Assert.That(testZone.ZoneNumber.Equals(zoneNum));
        }

        [Test]
        public void Test2()
        {
            string zoneNum = "1A";
            long objectId = 123;
            Zone testZone = new Zone(new WBObjectId(objectId), zoneNum);
            Assert.That(testZone.ObjectId.Equals(new WBObjectId(objectId)));
        }

        [Test]
        public void Test3()
        {
            string zoneNum = "1A";
            long objectId = 123;
            int zoneColor = 1;
            Zone testZone = new Zone(new WBObjectId(objectId), zoneNum);
            int actual = testZone.Color;
            Assert.That(testZone.Color.Equals(zoneColor));
        }
    }
}