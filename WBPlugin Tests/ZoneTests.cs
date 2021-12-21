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
    }
}