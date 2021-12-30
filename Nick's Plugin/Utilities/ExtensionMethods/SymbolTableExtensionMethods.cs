using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public static class SymbolTableExtensionMethods
    {
        public static ObjectId GetId(this SymbolTable table, string name)
        {
            ObjectId id = ObjectId.Null;
            using(Transaction tr = table.Database.TransactionManager.StartTransaction())
            {
                if (table.Has(name))
                {
                    id = table[name];
                    if (!id.IsErased) return id;
                    foreach (ObjectId recId in table)
                    {
                        if (!recId.IsErased)
                        {
                            SymbolTableRecord rec = (SymbolTableRecord)tr.GetObject(recId, OpenMode.ForRead);
                            if (string.Compare(rec.Name, name, true) == 0)
                                return recId;
                        }
                    }
                }
            }

            return id;
        }
    }
}
