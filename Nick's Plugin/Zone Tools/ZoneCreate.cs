using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            ColorManager.ChangeColor(zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));

            zone.Thermostat = StringInputRetriever.GetUserInput("\nEnter Thermostat Type: ", zone.Thermostat).ToUpper();

            manager.Add(zone);

            return true;
        }

        
    }
}
