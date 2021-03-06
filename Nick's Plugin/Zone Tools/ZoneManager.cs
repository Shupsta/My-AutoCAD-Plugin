using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using WarmBoardTools.Interfaces;
using WBPlugin.Utilities.ExtensionMethods;

namespace WBPlugin.Zone_Tools
{
    public class ZoneManager
    {
        private readonly string WBDictionaryName = "WBPLUGIN_ZONES";
        private readonly string ZoneRecordName = "WBPLUGIN_ZONERECORD";
        private List<Zone> _zoneList = new List<Zone>();

        public int NumberOfZones { get => _zoneList.Count; }

        public ZoneManager()
        {
            //OldZoneInfo();//TODO remove if tests prove unsuccessful, as well as old support folder and contents
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
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

                FillZoneList(zoneRecord.Data);
                
                
                

                tr.Commit();
            }                        

        }

        private void OldZoneInfo()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                DBDictionary NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForRead, false);
                DBDictionary WBDict;
                if (NOD.Contains("WB_SETTINGS"))
                {
                    WBDict = (DBDictionary)tr.GetObject(NOD.GetAt("WB_SETTINGS"), OpenMode.ForRead, false);
                    if (WBDict.Contains("ZONES"))
                    {
                        Xrecord zoneRecord = (Xrecord)tr.GetObject(WBDict.GetAt("ZONES"), OpenMode.ForRead, false);
                    }
                    

                }

                if (NOD.Contains("WB_SETTINGS_VERSION_2"))
                {
                    WBDict = (DBDictionary)tr.GetObject(NOD.GetAt("WB_SETTINGS_VERSION_2"), OpenMode.ForRead, false);
                    if (WBDict.Contains("ZONES_VERSION_2"))
                    {
                        Xrecord zoneRecord = (Xrecord)tr.GetObject(WBDict.GetAt("ZONES_VERSION_2"), OpenMode.ForRead, false);
                        var test = (List<IZone>)Old_Support.DeserializerForOldZoneData.Deserialize(zoneRecord.Data);
                    }
                }

                
            }
        }

        private void FillZoneList(ResultBuffer data)
        {
            if (data == null) return;
            TypedValue[] zoneData = data.AsArray();

            for(int i = 0; i+3 < zoneData.Length; i += 4)
            {
                
                long zoneHandle = (long)zoneData[i].Value;
                int zoneNum = (int)zoneData[i+1].Value;
                string system = (string)zoneData[i+2].Value;
                string thermostat = (string)zoneData[i+3].Value;

                _zoneList.Add(new Zone(new WBObjectId(zoneHandle), zoneNum, system, thermostat));

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
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                _zoneList.Add(zone);
                CreateXRecord(tr);
                tr.Commit();
            }
        }

        public void Remove(Zone zone)
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                _zoneList.Remove(zone);
                CreateXRecord(tr);
                tr.Commit();
            }
        }

        public void Sync()
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                CreateXRecord(tr);
                tr.Commit();
            }
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

        private void CreateXRecord(Transaction tr)
        {
            DBDictionary NOD = (DBDictionary)tr.GetObject(Active.Database.NamedObjectsDictionaryId, OpenMode.ForWrite, false);


            DBDictionary WBDict = (DBDictionary)tr.GetObject(NOD.GetAt(WBDictionaryName), OpenMode.ForWrite, false);

            Xrecord zoneRecord = (Xrecord)tr.GetObject(WBDict.GetAt(ZoneRecordName), OpenMode.ForWrite, false);

            ResultBuffer resbuf = new ResultBuffer();
            foreach (Zone zone in _zoneList)
            {

                resbuf.Add(new TypedValue((int)DxfCode.Int64, zone.ObjectId.Handle));
                resbuf.Add(new TypedValue((int)DxfCode.Int32, zone.ZoneNumber));
                resbuf.Add(new TypedValue((int)DxfCode.Text, zone.System));
                resbuf.Add(new TypedValue((int)DxfCode.Text, zone.Thermostat));
            }

            zoneRecord.Data = resbuf;
        }

        public void ForEach(Action<Zone> action)
        {
            foreach(Zone zone in _zoneList)
            {
                action(zone);
            }
        }

        public string IsInZone(WBPoint3d point)
        {
            string answer = null;
            using(DocumentLock docLoc = Active.Document.LockDocument())
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ForEach(zone =>
                {
                    WBObjectId wbId = (WBObjectId)zone.ObjectId;
                    Polyline zoneline = tr.GetObject(wbId.GetId(), OpenMode.ForRead, false) as Polyline;

                    if (!zoneline.Closed) zoneline.Closed = true;

                    if (zoneline.IsPointInside(point.GetPoint())) answer = zone.ZoneId;
                });
            }

            return answer;
        }

    }
}
