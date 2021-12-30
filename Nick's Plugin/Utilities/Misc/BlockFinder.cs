using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities.Misc
{
    public static class BlockFinder
    {
        public static bool FindBlock(string blockName)
        {
            Database db = Active.Database;
            bool success = true;

            using (DocumentLock dimlock = Active.Document.LockDocument())
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    ObjectId id = ObjectId.Null;
                    BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                    BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

                    id = bt.GetId(blockName);

                    //TODO if the block is not found, figure out how to find it in block resource file
                    
                    tr.Commit();
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    tr.Abort();
                    Active.WriteMessage("\nProblem occured because " + ex.Message.ToString());
                    success = false;
                }
            }
            return success;
        }
    }
}
