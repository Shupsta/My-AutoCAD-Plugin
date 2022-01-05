using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using WBPlugin.Loop_Tools;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneModifiedEvent
    {
        public static void ZoneModified(object sender, EventArgs e)
        {
            Polyline zoneLine = sender as Polyline;

            ChangeColor(zoneLine);




        }

        private static void ChangeColor(Polyline zoneLine)
        {
            ZoneManager manager = WBPlugin.ZoneManager;

            WBObjectId wbId = new WBObjectId(zoneLine.ObjectId.Handle.Value);

            Zone zoneObj = manager.Contains(wbId);
            if (zoneObj == null) return;

             Transaction test = zoneLine.Database.TransactionManager.TopTransaction;

            using (Transaction tr = test == null ?
                zoneLine.ObjectId.Database.TransactionManager.StartTransaction() : test)
            {
                int properColor = ColorManager.GetColorForZone(zoneObj.ZoneNumber);
                int actualColor = zoneLine.ColorIndex;
                if (properColor == actualColor) return;

                Editor ed = Active.Editor;
                TypedValue[] array = new TypedValue[1];
                array.SetValue(new TypedValue((int)DxfCode.BlockName, LoopMarker.LoopMarkerBlockName), 0);
                SelectionFilter filter = new SelectionFilter(array);

                PromptSelectionResult psr = ed.SelectAll(filter);

                if (psr.Status != PromptStatus.OK) return;

                foreach(ObjectId markerId in psr.Value.GetObjectIds())
                {
                    BlockReference marker = markerId.GetObject(OpenMode.ForRead, false) as BlockReference;
                    string containingZone = manager.IsInZone(new WBPoint3d(marker.Position.X, marker.Position.Y));
                    if(!zoneObj.ZoneId.Equals(containingZone)) continue;
                    LoopMarker.ChangeXDataLoopColor(marker, tr, actualColor);
                }

                tr.Commit();
            }
        }

        
    }
}
