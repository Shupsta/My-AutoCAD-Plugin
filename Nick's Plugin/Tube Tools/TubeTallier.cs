using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Tube_Tools
{
    public class TubeTallier
    {
        public static void Tally()
        {
            WBEntity tubeEntity = EntityInputRetriever.GetUserInput("\nSelect a tube entity");
            if (tubeEntity.IsNull()) return;

            TubeManager manager = new TubeManager();
            if (!manager.IsTube(tubeEntity)) return;

        }
    }
}
