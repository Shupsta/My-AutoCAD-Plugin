using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Tube_Tools;
using WBPlugin.Utilities.Misc;

namespace WBPlugin.Loop_Tools
{
    public class LoopMarker
    {
        public static void Add(PointInputRetriever pointRetriever)
        {
            Tube tube = TubeManager.GetTube();
            if (tube == null) return;

            WBPoint3d insertPoint = pointRetriever.GetUserInput("\nSelect insertion point: ", false);
            if (insertPoint.IsNull()) return;

            WBLayerTableRecord loopMarkerLayer = new WBLayerTableRecord("T_Loop_Marker");

            BlockInsert.Insert("Marker", insertPoint, loopMarkerLayer, 1);


        }
    }
}
