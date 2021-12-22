using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneCreater
    {
        public static bool AddZone()
        {
            //move all to a class ZoneCreater
            WBObjectId selectedPolyline = getPolyline();
            if (selectedPolyline == null) return false;

            ZoneManager manager = new ZoneManager();

            string defaultValue = manager.GetNextZoneNumber();
            string zoneId = StringInputRetriever.GetUserInput("\nEnter Zone Number: ", defaultValue);
            if (zoneId == "-99999") return false;

            Zone zone = new Zone(selectedPolyline, zoneId);

            ColorManager.ChangeColor(zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));

            zone.Thermostat = StringInputRetriever.GetUserInput("\nEnter Thermostat Type: ", zone.Thermostat).ToUpper();

            manager.Add(zone);

            return true;
        }

        private static WBObjectId getPolyline()
        {
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to make a Zone: ");
            if (selectedPolyLine.IsNull()) return null;
            return selectedPolyLine;
        }
    }
}
