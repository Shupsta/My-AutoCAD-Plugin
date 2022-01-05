using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Zone_Tools
{
    public static class ZoneDelete
    {
        public static bool Delete()
        {
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect Zone to Delete: ");
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
                manager.Remove(zone);
                Active.WriteMessage("\nZone Deleted!");
                return true;
            }
        }
    }
}
