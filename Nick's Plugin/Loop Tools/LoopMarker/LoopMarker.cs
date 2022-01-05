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
using WBPlugin.Zone_Tools;

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

                    ZoneManager manager = new ZoneManager();
                    string zoneId = manager.IsInZone(insertPoint);
                    if (zoneId == null) zoneId = "0";

                    Dictionary<string, string> attributeValues = new Dictionary<string, string>();
                    attributeValues.Add("ZONE", zoneId);
                    attributeValues.Add("MANIFOLD", LastManifold);
                    attributeValues.Add("LENGTH", length);
                    attributeValues.Add("LENGTH_ADD", AdditionalLength.ToString());
                    attributeValues.Add("LOOP", LastLoop.ToString());
                    attributeValues.Add("ROOM", RoomName);
                    attributeValues.Add("LENGTH_DISPLAY", (lengthNum + AdditionalLength).ToString() + "'");

                    block.UpdateAttributes(attributeValues);

                    ChangeLoopColor(loop, zoneId);
                    AddXData(block, tube, tr);

                    tr.Commit();
                }
                catch (Exception ex)
                {
                    Active.WriteMessage("\nError in LoopMarker.SetBlockAttribute() :" + ex.Message);
                    tr.Abort();
                }
            }
        }
        

        private static void ChangeLoopColor(Loop loop, string zoneId)
        {
            int color = 1;

            if (!zoneId.Equals("0"))
            {
                Match match = Regex.Match(zoneId, @"\d+");
                if (match.Success) color = Convert.ToInt32(match.Value);
                color = ColorManager.GetColorForZone(color);
            }

            ColorManager.ChangeColors(loop.GetCollectionForColor(), color);
        }

        private static void AddXData(BlockReference block, Tube tube, Transaction tr)
        {
            ResultBuffer resultbuf = new ResultBuffer();
            resultbuf.Add(new TypedValue((int)DxfCode.Handle, tube.Entity.ObjectId.Handle));
            Xrecord record = new Xrecord();
            record.Data = resultbuf;

            
            if(block.ExtensionDictionary == ObjectId.Null)
            {
                block.UpgradeOpen();
                block.CreateExtensionDictionary();
                
            }
            DBDictionary exDict = block.ExtensionDictionary.GetObject(OpenMode.ForWrite, false) as DBDictionary;
            exDict.SetAt("LoopEnt", record);
            tr.AddNewlyCreatedDBObject(record, true);
            

        }
    }
}
