using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin.Utilities.ExtensionMethods
{
    public static class PolyLineExtensionMethods
    {
        public static bool IsPointInside(this Polyline pline, Point3d point)
        {
            double tolerance = Tolerance.Global.EqualPoint;
            using (MPolygon mpg = new MPolygon())
            {
                mpg.AppendLoopFromBoundary(pline, true, tolerance);
                return mpg.IsPointInsideMPolygon(point, tolerance).Count == 1;
            }
        }
    }
}
