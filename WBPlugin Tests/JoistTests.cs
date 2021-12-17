using NUnit.Framework;
using Moq;
using WBPlugin;

namespace WBPlugin_Tests
{
    public class JoistTests
    {
        private Mock<IDoubleInputRetriever> _mockDoubleRetriever;
        private Mock<IPointInputRetriever> _mockPointRetriever;
        private Mock<IAngleInputRetriever> _mockAngleRetriever;
        private Mock<IBoundaryInputRetriever> _mockBoundaryRetriever;
        private Mock<IJoistGenerator> _mockJoistGenerator;

        [SetUp]
        public void Setup()
        {
            _mockDoubleRetriever = new Mock<IDoubleInputRetriever>();
            _mockPointRetriever = new Mock<IPointInputRetriever>();
            _mockAngleRetriever = new Mock<IAngleInputRetriever>();
            _mockBoundaryRetriever = new Mock<IBoundaryInputRetriever>();
            _mockJoistGenerator = new Mock<IJoistGenerator>();

        }

        [Test]
        public void Test1()
        {
            double joistSpacing = 24;
            double startX = 1;
            double startY = 1;
            
            _mockDoubleRetriever.Setup<double>(p => p.getUserInput(It.IsAny<string>(), It.IsAny<double>())).Returns(joistSpacing);

            _mockPointRetriever.Setup<WBPoint3d>(p => p.getUserInput(It.IsAny<string>(), It.IsAny<bool>())).Returns(new WBPoint3d(startX, startY));

            _mockAngleRetriever.Setup<double>(p => p.getUserInput(It.IsAny<string>(), It.IsAny<WBPoint3d>())).Returns(90);

            //_mockBoundaryRetriever.Setup<WBObjectIdCollection>(p => p.getUserInput(It.IsAny<string>())).Returns(new WBObjectIdCollection(new System.Collections.Generic.List<long>() { 1}));
            _mockBoundaryRetriever.SetupSequence<WBObjectIdCollection>(p => p.getUserInput(It.IsAny<string>()))
                .Returns(new WBObjectIdCollection(new System.Collections.Generic.List<long>() { 1 }))
                .Returns(new WBObjectIdCollection());


            _mockJoistGenerator.Setup<bool>(p => p.RunJoists(It.IsAny<Joists>())).Returns(true);

            Joists testjoist = new Joists(_mockDoubleRetriever.Object, _mockPointRetriever.Object, _mockAngleRetriever.Object, _mockBoundaryRetriever.Object, _mockJoistGenerator.Object);

            Assert.AreEqual(joistSpacing, testjoist.JoistSpacing);
        }
    }
}