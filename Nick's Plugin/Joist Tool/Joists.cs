using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin
{
    public class Joists
    {
        private double _joistSpacing;
        private WBPoint3d _startPoint;
        private double _angle;
        private WBObjectIdCollection _outerBoundary;
        private List<WBObjectIdCollection> _interiorBoundaries;
        private const string _joistLayerName = "B_Joists";
        private IDoubleInputRetriever _doubleRetriever;
        private IPointInputRetriever _pointRetriever;
        private IAngleInputRetriever _angleRetriever;
        private IBoundaryInputRetriever _boundaryInputRetriever;

        public Joists()
        {
            _joistSpacing = 16;
            _doubleRetriever = new DoubleInputRetriever();
            _pointRetriever = new PointInputRetriever();
            _angleRetriever = new AngleInputRetriever();
            _boundaryInputRetriever = new BoundaryInputRetriever();
            this.GetUserInput();
            WBLayerTableRecord joistLayer = GetOrCreateLayer();
            if (joistLayer == null) return;
            RunJoists();
        }

        public Joists(IDoubleInputRetriever spacingRetriever, IPointInputRetriever startPointRetriever, IAngleInputRetriever angleRetriever, IBoundaryInputRetriever boundaryRetriever)
        {
            _joistSpacing = 16;
            _doubleRetriever = spacingRetriever;
            _pointRetriever = startPointRetriever;
            _angleRetriever = angleRetriever;
            _boundaryInputRetriever = boundaryRetriever;
            this.GetUserInput();
            WBLayerTableRecord joistLayer = GetOrCreateLayer();
            if (joistLayer == null) return;
            RunJoists();
        }

        #region getters
        public double JoistSpacing { get => _joistSpacing;}
        public WBPoint3d StartPoint { get => _startPoint;}
        public double Angle { get => _angle;}
        public WBObjectIdCollection OuterBoundary { get => _outerBoundary;}
        public List<WBObjectIdCollection> InteriorBoundaries { get => _interiorBoundaries; }
        public string JoistLayer { get => _joistLayerName; }
        #endregion
        #region setters
        public DoubleInputRetriever SpacingRetriever { set => _doubleRetriever = value; }
        public PointInputRetriever StartPointRetriever { set => _pointRetriever = value; }
        public AngleInputRetriever AngleRetriever { set => _angleRetriever = value; }
        public BoundaryInputRetriever boundaryInputRetriever { set => _boundaryInputRetriever = value; }
        #endregion

        #region UserInput
        public void GetUserInput()
        {
            double joistSpacing = GetJoistSpacingInput();
            if (joistSpacing == 0) return;
            
            WBPoint3d startPoint = GetStartPointInput();
            if (startPoint.isNull()) return;

            double joistAngle = GetJoistAngleInput(startPoint);
            if (joistAngle == -9999) return;

            WBObjectIdCollection outerBoundary = GetOuterBoundaryInput();
            if (outerBoundary.IsNull()) return;
                                    
            
            _joistSpacing = joistSpacing;
            _startPoint = startPoint;
            _angle = joistAngle;
            _outerBoundary = outerBoundary;
            _interiorBoundaries = GetInteriorBoundaries();


        }               

        private double GetJoistSpacingInput()
        {            
            return  _doubleRetriever.getUserInput("\nEnter Joist center-to-center spacing :", _joistSpacing);
        }

        private WBPoint3d GetStartPointInput()
        {
            return _pointRetriever.getUserInput("\nStart point of first joist: ", false);
        }

        private double GetJoistAngleInput(WBPoint3d startpoint)
        {
            return _angleRetriever.getUserInput("\nPick direction of first joist: ", startpoint);
        }

        private WBObjectIdCollection GetOuterBoundaryInput()
        {
            return _boundaryInputRetriever.getUserInput("Select Outer Hatch Boundary: ");
        }

        private List<WBObjectIdCollection> GetInteriorBoundaries()
        {
            List<WBObjectIdCollection> returnList = new List<WBObjectIdCollection>();

            int numberOfBoundaries = 1;
            while (true)
            {
                var interiorBoundary = _boundaryInputRetriever.getUserInput("Select Interior Hatch Boundary " +
                                                 numberOfBoundaries.ToString() +
                                                 " (or ENTER): ");

                if (!interiorBoundary.IsNull())
                {
                    returnList.Add(interiorBoundary);
                    numberOfBoundaries++;
                }
                else
                {
                    break;
                }
            }
            return returnList;
        }
        #endregion

        private WBLayerTableRecord GetOrCreateLayer()
        {
            return new WBLayerTableRecord(LayerGenerator.CreateOrGetLayer(_joistLayerName));
        }

        protected virtual void RunJoists()
        {
            JoistGenerator generator = new JoistGenerator();
            generator.RunJoists(this);
        }
    }
}
