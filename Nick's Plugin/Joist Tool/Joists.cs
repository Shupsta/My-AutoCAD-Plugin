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
        private IDoubleInputRetriever _doubleRetriever;
        private IPointInputRetriever _pointRetriever;
        private IAngleInputRetriever _angleRetriever;
        private IBoundaryInputRetriever _boundaryInputRetriever;
        private bool _success;

        
        public Joists(IDoubleInputRetriever spacingRetriever, IPointInputRetriever startPointRetriever, IAngleInputRetriever angleRetriever, IBoundaryInputRetriever boundaryRetriever, IJoistGenerator joistGenerator)
        {
            _joistSpacing = 16;
            _doubleRetriever = spacingRetriever;
            _pointRetriever = startPointRetriever;
            _angleRetriever = angleRetriever;
            _boundaryInputRetriever = boundaryRetriever;
            this.GetUserInput();
            _success = joistGenerator.RunJoists(this);
        }

        #region getters
        public double JoistSpacing { get => _joistSpacing;}
        public WBPoint3d StartPoint { get => _startPoint;}
        public double Angle { get => _angle;}
        public WBObjectIdCollection OuterBoundary { get => _outerBoundary;}
        public List<WBObjectIdCollection> InteriorBoundaries { get => _interiorBoundaries; }
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

                if (interiorBoundary.IsNull()) break;
                
                returnList.Add(interiorBoundary);
                numberOfBoundaries++;
                
            }
            return returnList;
        }
        #endregion

        
    }
}
