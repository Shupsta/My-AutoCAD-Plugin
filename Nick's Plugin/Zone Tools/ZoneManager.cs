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

                _zoneList = new List<Zone>();
                FillZoneList(WBDict, tr);

                tr.Commit();
            }
                
        }

        private void FillZoneList(DBDictionary zoneDictionary, Transaction tr)
        {
            foreach(DBDictionaryEntry dbEntry in zoneDictionary)
            {
                Xrecord zoneRecord = (Xrecord)tr.GetObject(dbEntry.Value, OpenMode.ForRead, false);
                TypedValue[] zoneData = zoneRecord.Data.AsArray();
                long zoneHandle = (long)zoneData[0].Value;
                int zoneNum = (int)zoneData[1].Value;
                string system = (string)zoneData[2].Value;
                string thermostat = (string)zoneData[3].Value;

                Add(new Zone(new WBObjectId(zoneHandle), zoneNum, system, thermostat));

            }
        }

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

        public Zone Contains(WBObjectId id)
        {
            Zone zone = null;

            foreach(Zone z in _zoneList)
            {
                zone = z.Equals(id) ? z : null;
            }

            return zone;
        }

        private void CreateXRecord()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
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

                foreach(Zone zone in _zoneList)
                {
                    ResultBuffer resbuf = new ResultBuffer();
                    resbuf.Add(new TypedValue((int)DxfCode.Int64, zone.ObjectId.Handle));
                    resbuf.Add(new TypedValue((int)DxfCode.Int32, zone.ZoneNumber));
                    resbuf.Add(new TypedValue((int)DxfCode.Text, zone.System));
                    resbuf.Add(new TypedValue((int)DxfCode.Text, zone.Thermostat));

                    Xrecord zoneRecord = new Xrecord() { Data = resbuf };

                    WBDict.SetAt(zone.ZoneId, zoneRecord);
                }

                tr.Commit();
            }
        }
    }
}
