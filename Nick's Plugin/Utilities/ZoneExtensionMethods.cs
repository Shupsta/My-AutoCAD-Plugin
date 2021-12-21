using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace WBPlugin.Utilities
{
    public static class ZoneExtensionMethods
    {
        static class Assert
        {
            public static void IsNotNull<T>(T obj, string paramName) where T : class
            {
                if (obj == null)
                    throw new ArgumentNullException(paramName);
            }
        }

        public static Transaction GetTopTransaction(this Database db)
        {
            Assert.IsNotNull(db, nameof(db));
            return db.TransactionManager.TopTransaction ??
            throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.NoActiveTransactions);
        }

        public static DBDictionary TryGetExtensionDictionary(this DBObject source)
        {
            Assert.IsNotNull(source, nameof(source));
            var tr = source.Database.GetTopTransaction();
            ObjectId dictId = source.ExtensionDictionary;
            if (dictId.IsNull)
            {
                return null;
            }
            return (DBDictionary)tr.GetObject(dictId, OpenMode.ForRead);
        }

        public static DBDictionary GetOrCreateExtensionDictionary(this DBObject source)
        {
            Assert.IsNotNull(source, nameof(source));
            var tr = source.Database.GetTopTransaction();
            if (source.ExtensionDictionary == ObjectId.Null)
            {
                source.UpgradeOpen();
                source.CreateExtensionDictionary();
            }
            return (DBDictionary)tr.GetObject(source.ExtensionDictionary, OpenMode.ForRead);
        }

        public static ResultBuffer GetXrecordData(this DBObject source, string key)
        {
            Assert.IsNotNull(source, nameof(source));
            var tr = source.Database.GetTopTransaction();
            DBDictionary dict;
            if (source is DBDictionary)
            {
                dict = (DBDictionary)source;
            }
            else
            {
                dict = source.TryGetExtensionDictionary();
                if (dict == null)
                    return null;
            }
            if (!dict.Contains(key))
            {
                return null;
            }
            var xrec = tr.GetObject(dict.GetAt(key), OpenMode.ForRead) as Xrecord;
            if (xrec == null)
            {
                return null;
            }
            return xrec.Data;
        }

        public static void SetXrecordData(this DBObject source, string key, ResultBuffer data)
        {
            Assert.IsNotNull(source, nameof(source));
            var tr = source.Database.GetTopTransaction();
            DBDictionary dict;
            if (source is DBDictionary)
            {
                dict = (DBDictionary)source;
            }
            else
            {
                dict = source.GetOrCreateExtensionDictionary();
            }
            Xrecord xrec;
            if (dict.Contains(key))
            {
                xrec = tr.GetObject(dict.GetAt(key), OpenMode.ForWrite) as Xrecord;
                if (xrec == null)
                {
                    throw new Autodesk.AutoCAD.Runtime.Exception(ErrorStatus.InvalidKey, key);
                }
            }
            else
            {
                dict.UpgradeOpen();
                xrec = new Xrecord();
                dict.SetAt(key, xrec);
                tr.AddNewlyCreatedDBObject(xrec, true);
            }
            xrec.Data = data;
        }
    }
}
