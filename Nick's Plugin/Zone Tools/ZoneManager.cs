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
        private string ZoneRecordName = "WBPLUGIN_ZONERECORD";
        private List<Zone> _zoneList = new List<Zone>();

        public ZoneManager()
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                DBDictionary NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForRead, false);
                DBDictionary WBDict;
                if (!NOD.Contains(WBDictionaryName))
                {
                    DBDictionary newDict = new DBDictionary();
                    NOD.UpgradeOpen();
                    NOD.SetAt(WBDictionaryName, newDict);
                    tr.AddNewlyCreatedDBObject(newDict, true);
                }
                
                WBDict = (DBDictionary)tr.GetObject(NOD.GetAt(WBDictionaryName), OpenMode.ForRead, false);
                
                if (!WBDict.Contains(ZoneRecordName))
                {
                    Xrecord newValue = new Xrecord();
                    WBDict.UpgradeOpen();
                    WBDict.SetAt(ZoneRecordName, newValue);
                    tr.AddNewlyCreatedDBObject(newValue, true);
                }
                Xrecord zoneRecord = (Xrecord)tr.GetObject(WBDict.GetAt(ZoneRecordName), OpenMode.ForRead, false);

                
                if (zoneRecord.Data == null)
                {
                    tr.Commit();
                    return;
                }
                TypedValue[] zoneData = zoneRecord.Data.AsArray();

                foreach(var data in zoneData)
                {
                    _zoneList.Add((Zone)data.Value);
                }
                

                tr.Commit();
            }

            

        }

        //private void FillZoneList()
        //{
        //    foreach(DBDictionaryEntry dbEntry in zoneDictionary)
        //    {
        //        Xrecord zoneRecord = (Xrecord)tr.GetObject(dbEntry.Value, OpenMode.ForRead, false);
        //        TypedValue[] zoneData = zoneRecord.Data.AsArray();
        //        long zoneHandle = (long)zoneData[0].Value;
        //        int zoneNum = (int)zoneData[1].Value;
        //        string system = (string)zoneData[2].Value;
        //        string thermostat = (string)zoneData[3].Value;

        //        _zoneList.Add(new Zone(new WBObjectId(zoneHandle), zoneNum, system, thermostat));

        //    }
        //}

        public string GetNextZoneNumber()
        {
            if (_zoneList.Count == 0) return "1";
            Zone lastAdded = _zoneList.Last<Zone>();
            
            int number = lastAdded.ZoneNumber + 1;
            if(lastAdded.System != null)
            {
                return number.ToString() + lastAdded.System;
            }
            else
            {
                return number.ToString();
            }
            
            
        }

        public void Add(Zone zone)
        {
            _zoneList.Add(zone);
            CreateXRecord();
        }

        public void Remove(Zone zone)
        {
            _zoneList.Remove(zone);
            CreateXRecord();
        }

        public Zone Contains(WBObjectId id)
        {
            Zone zone = null;

            foreach(Zone z in _zoneList)
            {
                if (z.Equals(id))
                {
                    zone = z;
                    break;
                }                
            }

            return zone;
        }

        private void CreateXRecord()
        {            
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                DBDictionary NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForWrite, false);
                

                DBDictionary WBDict = (DBDictionary)tr.GetObject(NOD.GetAt(WBDictionaryName), OpenMode.ForWrite, false);

                Xrecord zoneRecord = (Xrecord)tr.GetObject(WBDict.GetAt(ZoneRecordName), OpenMode.ForWrite, false);

                ResultBuffer resbuf = new ResultBuffer();
                foreach (Zone zone in _zoneList)
                {
                    
                    resbuf.Add(new TypedValue(1, zone));
                }

                zoneRecord.Data = resbuf;

                tr.Commit();
            }
        }
    }
}
