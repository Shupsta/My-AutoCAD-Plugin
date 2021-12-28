using NUnit.Framework;
using Moq;
using WBPlugin;
using WBPlugin.Zone_Tools;
using System;

namespace WBPlugin_Tests
{
    public class ZoneTests
    {
        

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public void IsZoneIdCorrect()
        {
            string zoneNum = "1A";
            Zone testZone = new(new WBObjectId(123), zoneNum);
            Assert.That(testZone.ZoneId == zoneNum);
        }

        [Test]
        public void IsObjectIdCorrect()
        {
            string zoneNum = "1A";
            long objectId = 123;
            Zone testZone = new Zone(new WBObjectId(objectId), zoneNum);
            Assert.That(testZone.ObjectId.Equals(new WBObjectId(objectId)));
        }

        [Test]
        public void IsSystemCorrect()
        {
            string zoneNum = "1";
            string system = "a";
            string zoneId = zoneNum + system;
            long objectId = 123;
            Zone testZone = new Zone(new WBObjectId(objectId), zoneId);
            Assert.That(testZone.System == system.ToUpper());
        }

        [Test]
        public void AreZoneIdComponentsCorrect()
        {
            string zoneNum = "10";
            string system = "a";
            string zoneId = zoneNum + system;
            long objectId = 123;
            Zone testZone = new Zone(new WBObjectId(objectId), zoneId);
            Assert.That(testZone.System == system.ToUpper());
            Assert.That(testZone.ZoneNumber == Convert.ToInt32(zoneNum));

        }
    }
}