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
        private IWBObjectIdCollection _outerBoundary;
        private List<IWBObjectIdCollection> _interiorBoundaries;
        private readonly IDoubleInputRetriever _doubleRetriever;
        private readonly IPointInputRetriever _pointRetriever;
        private readonly IAngleInputRetriever _angleRetriever;
        private readonly IBoundaryInputRetriever _boundaryInputRetriever;

        
        public Joists(IDoubleInputRetriever spacingRetriever, IPointInputRetriever startPointRetriever, IAngleInputRetriever angleRetriever, IBoundaryInputRetriever boundaryRetriever, IJoistGenerator joistGenerator)
        {
            _joistSpacing = 16;
            _doubleRetriever = spacingRetriever;
            _pointRetriever = startPointRetriever;
            _angleRetriever = angleRetriever;
            _boundaryInputRetriever = boundaryRetriever;
            if (this.GetUserInput() == false) return;
            joistGenerator.RunJoists(this);
        }

        #region getters
        public double JoistSpacing { get => _joistSpacing;}
        public WBPoint3d StartPoint { get => _startPoint;}
        public double Angle { get => _angle;}
        public IWBObjectIdCollection OuterBoundary { get => _outerBoundary;}
        public List<IWBObjectIdCollection> InteriorBoundaries { get => _interiorBoundaries; }
        #endregion

        #region UserInput
        public bool GetUserInput()
        {
            double joistSpacing = GetJoistSpacingInput();
            if (joistSpacing == 0) return false;
            
            WBPoint3d startPoint = GetStartPointInput();
            if (startPoint.IsNull()) return false;

            double joistAngle = GetJoistAngleInput(startPoint);
            if (joistAngle == -9999) return false;

            IWBObjectIdCollection outerBoundary = GetOuterBoundaryInput();
            if (outerBoundary.IsNull()) return false;
                                    
            
            _joistSpacing = joistSpacing;
            _startPoint = startPoint;
            _angle = joistAngle;
            _outerBoundary = outerBoundary;
            _interiorBoundaries = GetInteriorBoundaries();

            return true;
        }               

        private double GetJoistSpacingInput()
        {            
            return  _doubleRetriever.GetUserInput("\nEnter Joist center-to-center spacing :", _joistSpacing);
        }

        private WBPoint3d GetStartPointInput()
        {
            return _pointRetriever.GetUserInput("\nStart point of first joist: ", false);
        }

        private double GetJoistAngleInput(WBPoint3d startpoint)
        {
            return _angleRetriever.GetUserInput("\nPick direction of first joist: ", startpoint);
        }

        private IWBObjectIdCollection GetOuterBoundaryInput()
        {
            return _boundaryInputRetriever.GetUserInput("Select Outer Hatch Boundary: ");
        }

        private List<IWBObjectIdCollection> GetInteriorBoundaries()
        {
            List<IWBObjectIdCollection> returnList = new List<IWBObjectIdCollection>();

            int numberOfBoundaries = 1;
            while (true)
            {
                var interiorBoundary = _boundaryInputRetriever.GetUserInput("Select Interior Hatch Boundary " +
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
