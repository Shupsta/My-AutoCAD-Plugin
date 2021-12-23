﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneShift
    {
        public static bool Shift()
        {
            
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to check for Zone Info: ");
            if (selectedPolyLine.IsNull()) return false;

            ZoneManager manager = new ZoneManager();
            
            Zone zone = manager.Contains(selectedPolyLine);

            if (zone == null)
            {
                Active.WriteMessage("\n Is not a Zone!");
                return false;
            }

            string mode = KeywordInputRetriever.GetUserInput("\nAdding or Removing Zone(s) ");
            if (mode == "-99999") return false;

            int shiftNumber = IntegerInputRetriever.GetUserInput("\nShift by how many?", 1, manager.NumberOfZones);
            if (shiftNumber == 0) return false;

            //int color = ColorManager.GetColor(selectedPolyLine);
            //StringBuilder message = new StringBuilder();
            //message.AppendFormat("\n Zone Number: {0}\nZone Color : {1}\nThermostat Type: {2}", zone.ZoneId, color, zone.Thermostat);
            //Active.WriteMessage(message.ToString());
            return true;

        }

        
    }
}
