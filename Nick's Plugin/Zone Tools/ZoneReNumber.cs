using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneRenumber
    {
        public static bool Renumber()
        {
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect Zone to Renumber: ");
            if (selectedPolyLine.IsNull()) return false;
            ZoneManager manager = WBPlugin.ZoneManager;

            Zone zone = manager.Contains(selectedPolyLine);

            if (zone == null)
            {
                Active.WriteMessage("\n Is not a Zone!");
                return false;
            }
            else
            {
                
                string newId = StringInputRetriever.GetUserInput("\nEnter the new Zone Number: ", zone.ZoneId);
                if (newId == "-99999") return false;
                zone.ZoneId = newId;
                ColorManager.ChangeColor((WBObjectId)zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));
                string thermostat = StringInputRetriever.GetUserInput("\nEnter the new Zone Thermostat Type: ", zone.Thermostat);
                if (thermostat == "-99999") return false;
                zone.Thermostat = thermostat;
                manager.Sync();
                return true;
            }
        }
    }
}
