using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin;

namespace WBPluginTests.Fakes
{
    class FakePointInputRetriever : PointInputRetriever
    {
        private double X { get; set; }
        private double Y { get; set; }
        
        public FakePointInputRetriever()
        {

        }

        public FakePointInputRetriever(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public override WBPoint3d getUserInput(string prompt, bool allowNone)
        {
            return new WBPoint3d(this.X, this.Y);
        }
    }
}
