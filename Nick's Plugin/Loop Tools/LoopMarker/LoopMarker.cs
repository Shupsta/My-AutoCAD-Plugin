using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Tube_Tools;
using WBPlugin.Utilities;
using WBPlugin.Utilities.ExtensionMethods;

namespace WBPlugin.Loop_Tools
{
    public static class LoopMarker
    {
        private static int _lastLoop = 1;
        
        public static string LoopMarkerBlockName { get; } = "MARKER";
        public static string LastManifold { get; set; } = "1";
        public static int LastLoop
        {
            get
            {
                if (_lastLoop > 8) return 1;
                return _lastLoop;
            }
            set
            {
                _lastLoop = value;
            }
        }
        public static string RoomName { get; set; }
        public static int AdditionalLength { get; set; } = 0;


        public static void Add(PointInputRetriever pointRetriever)
        {
            Tube tube = TubeManager.GetTube();
            if (tube == null) return;

            WBPoint3d insertPoint = pointRetriever.GetUserInput("\nSelect insertion point: ", false);
            if (insertPoint.IsNull()) return;

            WBLayerTableRecord loopMarkerLayer = new WBLayerTableRecord("T_Loop_Marker");

            ObjectId blockId = BlockInsert.Insert(LoopMarkerBlockName, insertPoint, loopMarkerLayer, 1);

            GetUserInput();

            SetBlockAttributes(tube, blockId);
        }

        private static void GetUserInput()
        {
            if (LastLoop == 0) LastLoop = 1;
            Application.ShowModalDialog(new LoopMarkerForm());
        }

        private static void SetBlockAttributes(Tube tube, ObjectId blockId)
        {
            Loop loop = new Loop(tube);

            string length = loop.GetLength();

            var match = Regex.Match(length, @"\d+");


            int lengthNum = match.Success ? Convert.ToInt32(match.Value) : 0; 

            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    BlockReference block = tr.GetObject(blockId, OpenMode.ForWrite, false) as BlockReference;

                    block.UpdateAttribute("ZONE", "1");//TODO update 1 to be a call to ZoneManager to find what zone the block is in
                    block.UpdateAttribute("MANIFOLD", LastManifold);
                    block.UpdateAttribute("LENGTH", length);
                    block.UpdateAttribute("LENGTH_ADD", AdditionalLength.ToString());
                    block.UpdateAttribute("LOOP", LastLoop.ToString());
                    block.UpdateAttribute("ROOM", RoomName);
                    block.UpdateAttribute("LENGTH_DISPLAY", (lengthNum + AdditionalLength).ToString() + "'");

                    tr.Commit();
                    Active.Document.SendStringToExecute("Regen ", true, false, false);
                }
                catch (Exception ex)
                {
                    Active.WriteMessage("\nError in LoopMarker.SetBlockAttribute() :" + ex.Message);
                    tr.Abort();
                }
            }
        }
    }
}
