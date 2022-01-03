using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using WarmBoardTools.Interfaces;

namespace WarmBoardTools.Interfaces.Zone
{
    [Serializable()]
    public class FakeZone : IZone
    {
        public string zoneNum { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string handle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int colorIndex => throw new NotImplementedException();

        public string thermostat { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsPtInZone(Point3d insPt)
        {
            throw new NotImplementedException();
        }

        public void UpdateColor()
        {
            throw new NotImplementedException();
        }
    }
}
