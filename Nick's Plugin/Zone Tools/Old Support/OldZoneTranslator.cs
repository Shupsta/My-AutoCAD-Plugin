using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Zone_Tools
{
    public class OldZoneTranslator
    {
        public static List<Zone> Translate(string dictionary, string xrecord)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                var NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForRead, false);

                var WBD = (DBDictionary)tr.GetObject(NOD.GetAt(dictionary), OpenMode.ForRead, false);

                var xrecordData = (Xrecord)tr.GetObject(WBD.GetAt(xrecord), OpenMode.ForRead, false);
            }

            return null;
        }
    }
}
