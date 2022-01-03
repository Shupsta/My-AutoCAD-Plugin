using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;

namespace WarmBoardTools.Interfaces
{
    
    public interface IZone
    {
        string zoneNum { get; set; }
        string handle { get; set; }
        int color { get; set; }
        int colorIndex { get; }

        string thermostat { get; set; }

        void UpdateColor();
        bool IsPtInZone(Point3d insPt);
        
    }
}
