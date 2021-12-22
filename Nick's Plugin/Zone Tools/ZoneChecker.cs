using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneChecker
    {
        public static bool CheckZone()
        {
            //move all to a class ZoneCreater
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to check for Zone Info: ");
            if (selectedPolyLine.IsNull()) return false;
            Active.WriteMessage("\n Color is : " + ColorManager.GetColor(selectedPolyLine).ToString());
            Zone zone = new ZoneManager().Contains(selectedPolyLine);

            if (zone == null)
            {
                Active.WriteMessage("\n Is not a Zone!");
                return false;
            }
            else
            {
                StringBuilder message = new StringBuilder();
                message.AppendFormat("\n Zone Number: {0}\nThermostat Type: {1}", zone.ZoneId, zone.Thermostat);
                Active.WriteMessage(message.ToString());
                return true;
            }
        }
    }
}
