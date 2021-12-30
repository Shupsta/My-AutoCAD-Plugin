using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;
using WBPlugin.Loop_Tools;

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

            Tube tube = new Tube(tubeEntity);

            Loop loop = new Loop(tube);

            HighlightTubes(loop);

            //TODO get info from loop marker for amount added

            StringBuilder report = new StringBuilder();
            report.AppendFormat("Horizontal Loop Length = {0}'", loop.GetLength());//TODO add custom groove and other info
            new Report(report.ToString());

            UnhighlightTubes(loop);
        }

        private static void HighlightTubes(Loop loop)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach (Tube tube in loop.Tubes)
                {
                    ObjectId id = tube.Entity.ObjectId.GetId();
                    Entity ent = (Entity)id.GetObject(OpenMode.ForRead, false);
                    ent.Highlight();
                }
            }
            
            
            Active.Editor.UpdateScreen();
        }

        private static void UnhighlightTubes(Loop loop)
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach (Tube tube in loop.Tubes)
                {
                    ObjectId id = tube.Entity.ObjectId.GetId();
                    Entity ent = (Entity)id.GetObject(OpenMode.ForRead, false);
                    ent.Unhighlight();
                }
            }


            Active.Editor.UpdateScreen();
        }
    }
}
