using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin;

namespace WBPluginTests.Fakes
{
    class FakeBoundaryInputRetriever : BoundaryInputRetriever
    {
        public override WBObjectIdCollection getUserInput(string prompt)
        {
            return new WBObjectIdCollection();
        }
    }
}
