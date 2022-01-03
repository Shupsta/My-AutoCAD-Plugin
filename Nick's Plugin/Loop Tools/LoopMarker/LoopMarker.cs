using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using WBPlugin.Tube_Tools;
using WBPlugin.Utilities;
using WBPlugin.Utilities.Misc;

namespace WBPlugin.Loop_Tools
{
    public class LoopMarker
    {
        public static string LoopMarkerBlockName { get; } = "Marker";
        
        public static void Add(PointInputRetriever pointRetriever)
        {
            Tube tube = TubeManager.GetTube();
            if (tube == null) return;

            WBPoint3d insertPoint = pointRetriever.GetUserInput("\nSelect insertion point: ", false);
            if (insertPoint.IsNull()) return;

            WBLayerTableRecord loopMarkerLayer = new WBLayerTableRecord("T_Loop_Marker");

            BlockInsert.Insert(LoopMarkerBlockName, insertPoint, loopMarkerLayer, 1);

            GetUserInput();
        }

        private static void GetUserInput()
        {
            Application.ShowModalDialog(new LoopMarkerForm());
        }
    }
}
