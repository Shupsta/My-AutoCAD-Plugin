using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin;

namespace WBPluginTests.Fakes
{
    class FakeAngleInputRetriever : AngleInputRetriever
    {
        private double Angle { get; set; }
        
        public FakeAngleInputRetriever()
        {

        }

        public FakeAngleInputRetriever(double angle)
        {
            this.Angle = angle;
        }
        public override double getUserInput(string prompt, WBPoint3d point)
        {
            return this.Angle;
        }
    }
}
