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
            var mockDoubleRetriever = new Mock<IDoubleInputRetriever>();
            mockDoubleRetriever.Setup<double>(p => p.getUserInput("Test Prompt", 16)).Returns(16);

            var mockPointRetriever = new Mock<IPointInputRetriever>();
            mockPointRetriever.Setup<WBPoint3d>(p => p.getUserInput("Test Prompt", false)).Returns(new WBPoint3d(1, 1));

            var mockAngleRetriever = new Mock<IAngleInputRetriever>();
            mockAngleRetriever.Setup<double>(p => p.getUserInput("test prompt", new WBPoint3d(1, 1))).Returns(90);

            var mockBoundaryRetriever = new Mock<IBoundaryInputRetriever>();
            mockBoundaryRetriever.Setup<WBObjectIdCollection>(p => p.getUserInput("Test prompt")).Returns(new WBObjectIdCollection());

            var mockJoistGenerator = new Mock<IJoistGenerator>();
            mockJoistGenerator.Setup<bool>(p => p.RunJoists(It.IsAny<Joists>())).Returns(true);

            Joists testjoist = new Joists(mockDoubleRetriever.Object, mockPointRetriever.Object, mockAngleRetriever.Object, mockBoundaryRetriever.Object, mockJoistGenerator.Object);
        }
    }
}