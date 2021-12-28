using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tube_Tools
{
    public class Tube : IEquatable<Tube>
    {
        private double _length;
        private bool? _isBuried;
        private WBPoint3d _start;
        private WBPoint3d _end;
        
        public WBEntity Entity { get; private set; }
        public string Name { get => Entity.TypeName; }
        public WBPoint3d Start { get
            {
                if (_start == null) SetStartAndEndPoints();
                return _start;
            } private set
            {
                _start = value;
            } }
        public WBPoint3d End
        {
            get
            {
                if (_end == null) SetStartAndEndPoints();
                return _end;
            } private set
            {
                _end = value;
            }
        }
        public double Length { get
            {
                if (_length != 0) return _length;
                return GetLength();
            } }
        public bool IsBuried { get
            {
                if (_isBuried != null) return (bool)_isBuried;
                return GetIsBuried();
            } }

        

        public Tube(WBEntity ent)
        {
            Entity = ent;
            



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
        private bool GetIsBuried()
        {
            bool isBuried = this.Entity.Layer.Contains("BURIED");
            this._isBuried = isBuried;
            return isBuried;
        }

        private void SetStartAndEndPoints()
        {
            Tuple<WBPoint3d, WBPoint3d> startAndEnd = TubeManager.GetStartOrEndPoint(this.Entity);
            Start = startAndEnd.Item1;
            End = startAndEnd.Item2;
        }
    }
}
