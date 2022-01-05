using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneCreate
    {
        public static bool AddZone()
        {
            
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to make a Zone: ");
            if (selectedPolyLine.IsNull()) return false;

            ZoneManager manager = new ZoneManager();

            string defaultValue = manager.GetNextZoneNumber();
            string zoneId = StringInputRetriever.GetUserInput("\nEnter Zone Number: ", defaultValue);
            if (zoneId == "-99999") return false;

            Zone zone = new Zone(selectedPolyLine, zoneId);

            ColorManager.ChangeColor((WBObjectId)zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));

            zone.Thermostat = StringInputRetriever.GetUserInput("\nEnter Thermostat Type: ", zone.Thermostat).ToUpper();

            manager.Add(zone);

            AddEvent(zone);

            return true;
        }

        public static void AddEvent(Zone zone)
        {
            ObjectId id = (zone.ObjectId as WBObjectId).GetId();

            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                Entity ent = id.GetObject(OpenMode.ForWrite, false) as Entity;

                ent.Modified += ZoneModifiedEvent.ZoneModified;

                tr.Commit();
            }
        }
    }
}
