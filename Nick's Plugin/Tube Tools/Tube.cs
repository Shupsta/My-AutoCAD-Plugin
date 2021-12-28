using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tube_Tools
{
    public class Tube : IEquatable<Tube>
    {
        public WBEntity Entity { get; private set; }
        public string Name { get => Entity.TypeName; }
        public WBPoint3d Start { get; private set; }
        public WBPoint3d End { get; private set; }
        public double Length { get; set; }
        public bool IsBuried { get; set; }

        public Tube(WBEntity ent)
        {
            Entity = ent;
            Start = TubeManager.GetStartOrEndPoint(ent, "start");
            End = TubeManager.GetStartOrEndPoint(ent, "end");
            Length = TubeManager.GetLength(ent);
            IsBuried = ent.Layer.Contains("BURIED");



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
    }
}
