using NUnit.Framework;
using Moq;
using WBPlugin;
using WBPlugin.Zone_Tools;
using System;

namespace WBPlugin_Tests
{
    public class ZoneTests
    {
        Mock<IWBObjectId> _objectId;

        [SetUp]
        public void Setup()
        {
            _objectId = new Mock<IWBObjectId>();

        }

        [Test]
        public void IsZoneIdCorrect()
        {
            string zoneNum = "1A";
            
            Zone testZone = new(_objectId.Object, zoneNum);
            Assert.That(testZone.ZoneId == zoneNum);
        }

        [Test]
        public void IsSystemCorrect()
        {
            string zoneNum = "1";
            string system = "a";
            string zoneId = zoneNum + system;
            long objectId = 123;
            Zone testZone = new Zone(_objectId.Object, zoneId);
            Assert.That(testZone.System == system.ToUpper());
        }

        [Test]
        public void AreZoneIdComponentsCorrect()
        {
            string zoneNum = "10";
            string system = "a";
            string zoneId = zoneNum + system;
            long objectId = 123;
            Zone testZone = new Zone(_objectId.Object, zoneId);
            Assert.That(testZone.System == system.ToUpper());
            Assert.That(testZone.ZoneNumber == Convert.ToInt32(zoneNum));

        }
    }
}