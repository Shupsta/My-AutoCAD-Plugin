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
        private Mock<IWBObjectId> _objectId;
        private Mock<IWBObjectIdCollection> _objectIdCollection;

        [SetUp]
        public void Setup()
        {
            _mockDoubleRetriever = new Mock<IDoubleInputRetriever>();
            _mockPointRetriever = new Mock<IPointInputRetriever>();
            _mockAngleRetriever = new Mock<IAngleInputRetriever>();
            _mockBoundaryRetriever = new Mock<IBoundaryInputRetriever>();
            _mockJoistGenerator = new Mock<IJoistGenerator>();
            _objectId = new Mock<IWBObjectId>();
            _objectIdCollection = new Mock<IWBObjectIdCollection>();
            _objectIdCollection.Setup(p => p.IdCollection).Returns(new System.Collections.Generic.List<IWBObjectId>() { _objectId.Object });

        }

        [Test]
        public void Test1()
        {
            double joistSpacing = 24;
            double startX = 1;
            double startY = 1;
            
            _mockDoubleRetriever.Setup<double>(p => p.GetUserInput(It.IsAny<string>(), It.IsAny<double>())).Returns(joistSpacing);

            _mockPointRetriever.Setup<WBPoint3d>(p => p.GetUserInput(It.IsAny<string>(), It.IsAny<bool>())).Returns(new WBPoint3d(startX, startY));

            _mockAngleRetriever.Setup<double>(p => p.GetUserInput(It.IsAny<string>(), It.IsAny<WBPoint3d>())).Returns(90);


            _mockBoundaryRetriever.SetupSequence(p => p.GetUserInput(It.IsAny<string>()))
                .Returns(new Mock<IWBObjectIdCollection>().Object)
                .Returns(new WBObjectIdCollection());


            _mockJoistGenerator.Setup<bool>(p => p.RunJoists(It.IsAny<Joists>())).Returns(true);

            Joists testjoist = new Joists(_mockDoubleRetriever.Object, _mockPointRetriever.Object, _mockAngleRetriever.Object, _mockBoundaryRetriever.Object, _mockJoistGenerator.Object);

            Assert.AreEqual(joistSpacing, testjoist.JoistSpacing);
        }
    }
}