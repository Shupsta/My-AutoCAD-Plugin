using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin
{
    public class WBPoint3d : IEquatable<WBPoint3d>
    {
        
        private readonly double _x;
        private readonly double _y;

        public WBPoint3d()
        {            
            _x = 0;
            _y = 0;
        }
                
        public WBPoint3d(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public bool IsNull()
        {
            if(X == 0 && Y == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Equals(WBPoint3d other)
        {
            if (Math.Round(X,2).Equals(Math.Round(other.X, 2)) && Math.Round(Y, 2).Equals(Math.Round(other.Y, 2))) return true;
            return false;
        }

        public Point3d GetPoint()
        {
            return new Point3d(this.X, this.Y, 0);
        }

        public double X { get => _x; }
        public double Y { get => _y; }

        
    }
}
