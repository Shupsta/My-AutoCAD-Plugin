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

            ZoneManager manager = new ZoneManager();

            WBObjectId wbId = new WBObjectId(zoneLine.ObjectId.Handle.Value);

            Zone zoneObj = manager.Contains(wbId);
            if (zoneObj == null) return;

            int properColor = ColorManager.GetColorForZone(zoneObj.ZoneNumber);
            int actualColor = ColorManager.GetColor(wbId);
            if (properColor == actualColor) return;

            ChangeColor(zoneLine.ObjectId);


        }

        private static void ChangeColor(ObjectId zoneId)
        {
            using(Transaction tr = zoneId.Database.TransactionManager.StartTransaction())
            {
                Editor ed = Active.Editor;

                TypedValue[] array = new TypedValue[1];
                array.SetValue(new TypedValue((int)DxfCode.BlockName, LoopMarker.LoopMarkerBlockName), 0);
                SelectionFilter filter = new SelectionFilter(array);

                PromptSelectionResult psr = ed.SelectAll(filter);

                if (psr.Status != PromptStatus.OK) return;
            }
        }
    }
}
