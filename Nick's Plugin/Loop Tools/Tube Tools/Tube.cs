using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tube_Tools
{
    public class Tube : IEquatable<Tube>
    {
        private double? _length;
        private bool? _isCustom;
        private WBPoint3d _start;
        private WBPoint3d _end;
        private WBPoint3d _middle;
        
        public WBEntity Entity { get; private set; }
        public string Name { get => Entity.TypeName; }
        public WBPoint3d Start { get
            {
                if (_start == null) SetPoints();
                return _start;
            } private set
            {
                _start = value;
            } }
        public WBPoint3d End
        {
            get
            {
                if (_end == null) SetPoints();
                return _end;
            } private set
            {
                _end = value;
            }
        }
        public WBPoint3d Middle
        {
            get
            {
                if (_middle == null) SetPoints();
                return _middle;
            }
            private set
            {
                _middle = value;
            }
        }

        public double Length { get
            {
                if (_length != null) return (double)_length;
                return GetLength();
            }private set
            {
                _length = value;
            }
        }
        public bool IsCustom { get
            {
                if (_isCustom != null) return (bool)_isCustom;
                return GetIsCustom();
            } }

        

        public Tube(WBEntity ent)
        {
            Entity = ent;            
        }
        public Tube(WBEntity ent, double length)
        {
            Entity = ent;
            this.Length = length;
        }

        public bool IsConnected(Tube that)
        {
            if (this.Start.Equals(that.Start)) return true;
            if (this.Start.Equals(that.End)) return true;
            if (this.End.Equals(that.Start)) return true;
            if (this.End.Equals(that.End)) return true;
            return false;
        }

        public bool Equals(Tube other)
        {
            if (this.Entity.Equals(other.Entity)) return true;
            return false;
        }

        private double GetLength()
        {
            double length = TubeManager.GetLength(this.Entity);
            this._length = length;
            return length;
        }
        private bool GetIsCustom()
        {
            bool isCustom = this.Entity.Layer.Contains("CUSTOM");
            this._isCustom = isCustom;
            return isCustom;
        }

        private void SetPoints()
        {
            Tuple<WBPoint3d, WBPoint3d, WBPoint3d> points = TubeManager.GetStartOrEndPoint(this.Entity);
            Start = points.Item1;
            End = points.Item2;
            Middle = points.Item3;
        }
    }
}
