﻿using System;
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
        public static string LoopMarkerBlockName { get; } = "MARKER";
        public static string LastManifold { get; set; } = "1";
        public static int LastLoop { get; set; } = 0;
        public static string RoomName { get; set; }
        public static int AdditionalLength { get; set; } = 0;


        public static void Add()
        {
            Tube tube = TubeManager.GetTube();
            if (tube == null) return;

            WBPoint3d insertPoint = tube.Middle;
            if (insertPoint.IsNull()) return;

            WBLayerTableRecord loopMarkerLayer = new WBLayerTableRecord("T_Loop_Marker");

            GetUserInput();

            SetBlockAttributes(tube, insertPoint, loopMarkerLayer);
        }

        private static void GetUserInput()
        {
            if (LastLoop+1 > 8) LastLoop = 1;
            Application.ShowModalDialog(new LoopMarkerForm());
        }

        private static void SetBlockAttributes(Tube tube, WBPoint3d insertPoint, WBLayerTableRecord loopMarkerLayer)
        {
            Loop loop = new Loop(tube);

            string length = loop.GetLength();

            var match = Regex.Match(length, @"\d+");


            int lengthNum = match.Success ? Convert.ToInt32(match.Value) : 0; 

            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                try
                {


                    ObjectId blockId = BlockInsert.Insert(LoopMarkerBlockName, insertPoint, loopMarkerLayer, 1);
                    BlockReference block = tr.GetObject(blockId, OpenMode.ForWrite, false) as BlockReference;

                    Dictionary<string, string> attributeValues = new Dictionary<string, string>();
                    attributeValues.Add("ZONE", "1");
                    attributeValues.Add("MANIFOLD", LastManifold);
                    attributeValues.Add("LENGTH", length);
                    attributeValues.Add("LENGTH_ADD", AdditionalLength.ToString());
                    attributeValues.Add("LOOP", LastLoop.ToString());
                    attributeValues.Add("ROOM", RoomName);
                    attributeValues.Add("LENGTH_DISPLAY", (lengthNum + AdditionalLength).ToString() + "'");

                    block.UpdateAttributes(attributeValues);                    

                    tr.Commit();
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
