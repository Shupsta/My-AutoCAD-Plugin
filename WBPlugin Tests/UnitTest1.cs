using NUnit.Framework;
using Moq;
using WBPlugin;

namespace WBPlugin_Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var mockDoubleRetriever = new Mock<DoubleInputRetriever>();
            mockDoubleRetriever.Setup<double>(p => p.getUserInput("Test Prompt", 16)).Returns(16);

            var mockPointRetriever = new Mock<PointInputRetriever>();
            mockPointRetriever.Setup<WBPoint3d>(p => p.getUserInput("Test Prompt", false)).Returns(new WBPoint3d(1, 1));

            var mockAngleRetriever = new Mock<AngleInputRetriever>();
            mockAngleRetriever.Setup<double>(p => p.getUserInput("test prompt", new WBPoint3d(1, 1))).Returns(90);

            var mockBoundaryRetriever = new Mock<BoundaryInputRetriever>();
            mockBoundaryRetriever.Setup<WBObjectIdCollection>(p => p.getUserInput("Test prompt")).Returns(new WBObjectIdCollection());

            Joists testjoist = new Joists(mockDoubleRetriever.Object, mockPointRetriever.Object, mockAngleRetriever.Object, mockBoundaryRetriever.Object);
        }
    }
}