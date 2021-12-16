using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin
{
    public class WBPoint3d
    {
        private Point3d _point;
        private double _x;
        private double _y;

        public WBPoint3d()
        {
            _point = new Point3d();
            _x = _point.X;
            _y = _point.Y;
        }
        
        public WBPoint3d(Point3d point)
        {
            _point = point;
            _x = _point.X;
            _y = _point.Y;
        }

        public WBPoint3d(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public bool isNull()
        {
            if(_point.X == 0 && _point.Y == 0 && _point.Z == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point3d getPoint()
        {
            return _point;
        }

        public double X { get => _x; }
        public double Y { get => _y; }
    }
}
