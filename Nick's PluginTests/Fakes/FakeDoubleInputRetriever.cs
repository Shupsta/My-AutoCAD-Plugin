using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin;

namespace WBPluginTests.Fakes
{
    public class FakeDoubleInputRetriever : DoubleInputRetriever
    {
        public double ReturnValue { get; set; }

        public FakeDoubleInputRetriever()
        {

        }
        public FakeDoubleInputRetriever(Double value)
        {
            ReturnValue = value;
        }
        
        
        
        public override double getUserInput(string prompt, double defaultValue)
        {
            return ReturnValue;
        }
    }
}
