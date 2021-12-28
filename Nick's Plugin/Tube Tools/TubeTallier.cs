using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

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


            Active.WriteMessage("\nAll Done!");
        }

        private static void HighlightTubes(Loop loop)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach (Tube tube in loop.Tubes)
                {
                    ObjectId id = ObjectIdTranslator.Decode(tube.Entity.ObjectId);
                    Entity ent = (Entity)id.GetObject(OpenMode.ForRead, false);
                    ent.Highlight();
                }
            }
            
            
            Active.Editor.UpdateScreen();
        }
    }
}
