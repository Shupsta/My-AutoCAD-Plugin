using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneCheck
    {
        public static bool CheckZone()
        {
            
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to check for Zone Info: ");
            if (selectedPolyLine.IsNull()) return false;
            
            Zone zone = WBPlugin.ZoneManager.Contains(selectedPolyLine);

            if (zone == null)
            {
                Active.WriteMessage("\n Is not a Zone!");
                return false;
            }
            else
            {
                int color = GetColor(selectedPolyLine);
                StringBuilder message = new StringBuilder();
                message.AppendFormat("\n Zone Number: {0}\nZone Color : {1}\nThermostat Type: {2}", zone.ZoneId, color, zone.Thermostat);
                Active.WriteMessage(message.ToString());
                return true;
            }
        }

        private static int GetColor(WBObjectId selectedLine)
        {
            using(Transaction tr = selectedLine.GetId().Database.TransactionManager.StartTransaction())
            {
                Entity line = tr.GetObject(selectedLine.GetId(), OpenMode.ForRead, false) as Entity;
                return line.ColorIndex;
            }
        }
    }
}
