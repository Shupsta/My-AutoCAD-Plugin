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

        public ZoneManager()
        {
            int dictionaryVersion = FindOldDictionary();
            bool hasZoneData = false;
            if (dictionaryVersion != 0)
            {
                hasZoneData = CheckIfDictionaryContainsZoneData(dictionaryVersion);
                OldZoneTranslator.Translate("WB_SETTINGS", "ZONES");
            }
            else
            {
                
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

        public static string GetNextZoneNumber()
        {
            return "1";
        }
    }
}
