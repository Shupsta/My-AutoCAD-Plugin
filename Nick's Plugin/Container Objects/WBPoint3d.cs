using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class WBPoint3d : IEquatable<WBPoint3d>
    {
        
        private double _x;
        private double _y;

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

        public bool isNull()
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
            if (X.Equals(other.X) && Y.Equals(other.Y)) return true;
            return false;
        }

        public double X { get => _x; }
        public double Y { get => _y; }

        
    }
}
