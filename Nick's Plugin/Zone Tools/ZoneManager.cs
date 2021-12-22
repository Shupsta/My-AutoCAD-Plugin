using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Zone_Tools
{
    public class ZoneManager
    {
        private string WBDictionaryName = "WBPLUGIN_ZONES";
        private List<Zone> _zoneList;

        public ZoneManager()
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                DBDictionary NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForRead, false);
                DBDictionary WBDict;
                if (NOD.Contains(WBDictionaryName))
            {
                    WBDict = (DBDictionary)tr.GetObject(NOD.GetAt(WBDictionaryName), OpenMode.ForRead, false);
            }
            else
            {
                    NOD.UpgradeOpen();
                    NOD.SetAt(WBDictionaryName, new DBDictionary());
                    WBDict = (DBDictionary)tr.GetObject(NOD.GetAt(WBDictionaryName), OpenMode.ForRead, false);
                
            }

                FillZoneList(WBDict, tr);
                

        }
                
        }

        private void FillZoneList(DBDictionary zoneDictionary, Transaction tr)
        {
            foreach(DBDictionaryEntry dbEntry in zoneDictionary)
            {
                Xrecord zoneRecord = (Xrecord)tr.GetObject(dbEntry.Value, OpenMode.ForRead, false);
                TypedValue[] zoneData = zoneRecord.Data.AsArray();
                long zoneHandle = (long)zoneData[1].Value;
                string zoneNum = (string)zoneData[2].Value;


            }
        }

        private int FindOldDictionary()//0 = none found, 1 = version1, 2 = version2
        {
            string version1DictionaryName = "WB_SETTINGS";
            string version2DictionaryName = "WB_SETTINGS_VERSION_2";
            bool hasVersion1 = false;
            bool hasVersion2 = false;

            Database db = Active.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary NamedObjectDictionary = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead, false);

                if (NamedObjectDictionary.Contains(version1DictionaryName))
                {
                    hasVersion1 = true;
                }

                if (NamedObjectDictionary.Contains(version2DictionaryName))
                {
                    hasVersion2 = true;
                }
            }

            if(hasVersion1 == false && hasVersion2 == false)
            {
                return 0;
            }else if (hasVersion1)
            {
                return 1;
            }else if (hasVersion2)
            {
                return 2;
            }
            else
            {
                return 2;
            }
        }

        private bool CheckIfDictionaryContainsZoneData(int dictionaryVersion)
        {
            string dictionaryName;
            string xrecordName;
            Database db = Active.Database;

            if(dictionaryVersion == 1)
            {
                dictionaryName = "WB_SETTINGS";
                xrecordName = "ZONES";
            }
            else
            {
                dictionaryName = "WB_SETTINGS_VERSION_2";
                xrecordName = "ZONES_VERSION_2";
            }

            using(Transaction tr = db.TransactionManager.StartTransaction())
            {
                DBDictionary NamedObjectDictionary = (DBDictionary)tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead, false);

                DBDictionary wbDictionary = (DBDictionary)tr.GetObject(NamedObjectDictionary.GetAt(dictionaryName), OpenMode.ForRead, false);

                if (wbDictionary.Contains(xrecordName)) return true;

            }

            return false;
        }

        public string GetNextZoneNumber()
        {
            return "1";
        }
    }
}
