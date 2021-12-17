using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin.Utilities
{
    public static class PointGenerator
    {
        internal static Point3d GetPoint(WBPoint3d point)
        {
            return new Point3d(point.X, point.Y, 0);
        }
    }
}
