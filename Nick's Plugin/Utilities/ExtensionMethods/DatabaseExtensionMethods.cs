using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace WBPlugin.Utilities
{
    public static class DatabaseExtensionMethods
    {
        public static void ForEach<T>(this Database db, Action<T> action) where T : DBObject
        {
            using(Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(db.BlockTableId, OpenMode.ForRead, false) as BlockTable;
                BlockTableRecord btr = tr.GetObject(db.CurrentSpaceId, OpenMode.ForRead, false) as BlockTableRecord;

                RXClass theClass = RXObject.GetClass(typeof(T));
                
                foreach(ObjectId id in btr)
                {
                    if (id.ObjectClass.IsDerivedFrom(theClass))
                    {
                        var entity = tr.GetObject(id, OpenMode.ForRead, false) as T;
                        action(entity);
                    }
                }

                tr.Commit();
            }
        }
    }
}
