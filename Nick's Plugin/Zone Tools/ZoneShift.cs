using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public class ZoneShift
    {
        private string _mode;
        private int _shiftNumber;
        private Zone _pivotZone;
        
        public bool Shift()
        {
            
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to check for Zone Info: ");
            if (selectedPolyLine.IsNull()) return false;

            ZoneManager manager = new ZoneManager();            
            _pivotZone = manager.Contains(selectedPolyLine);
            if (_pivotZone == null)
            {
                Active.WriteMessage("\n Is not a Zone!"); 
                return false;
            }

            string addMode = "Add";
            string removeMode = "Remove";
            _mode = KeywordInputRetriever.GetUserInput("\nAdding or Removing Zone(s) ", addMode, removeMode);
            if (_mode == "-99999") return false;

            _shiftNumber = IntegerInputRetriever.GetUserInput("\nShift by how many?", 1, manager.NumberOfZones);
            if (_shiftNumber == 0) return false;


            if(_mode == addMode)
            {
                manager.ForEach(ShiftZoneAdd);
                manager.Sync();
            }

            if (_mode == removeMode)
            {
                manager.ForEach(ShiftZoneRemove);
                ColorManager.ChangeColor(_pivotZone.ObjectId, 6);
                manager.Remove(_pivotZone);
            }
            

            //int color = ColorManager.GetColor(selectedPolyLine);
            //StringBuilder message = new StringBuilder();
            //message.AppendFormat("\n Zone Number: {0}\nZone Color : {1}\nThermostat Type: {2}", zone.ZoneId, color, zone.Thermostat);
            //Active.WriteMessage(message.ToString());
            return true;

        }

        private void ShiftZoneAdd(Zone zone)
        {
            if(zone.System == _pivotZone.System && zone.ZoneNumber >= _pivotZone.ZoneNumber)
            {
                int newZoneNum = zone.ZoneNumber + 1;
                zone.ZoneId = newZoneNum.ToString() + zone.System;
                ColorManager.ChangeColor(zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));
                
            }
        }

        private void ShiftZoneRemove(Zone zone)
        {
            if (zone.System == _pivotZone.System && zone.ZoneNumber > _pivotZone.ZoneNumber)
            {
                int newZoneNum = zone.ZoneNumber - 1;
                zone.ZoneId = newZoneNum.ToString() + zone.System;
                ColorManager.ChangeColor(zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));
            }
        }
    }
}
