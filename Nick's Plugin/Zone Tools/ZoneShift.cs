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
        private string _addMode = "Add";
        private string _removeMode = "Remove";
        private string _mode;
        private int _shiftNumber;
        private Zone _pivotZone;
        private ZoneManager _manager;
        
        public bool Shift()
        {
            this._manager = new ZoneManager();

            if (!GetPivotZone()) return false;

            if (!GetMode()) return false;

            _shiftNumber = IntegerInputRetriever.GetUserInput("\nShift by how many?", 1, _manager.NumberOfZones);
            if (_shiftNumber == 0) return false;

            if (_mode == _addMode)
            {
                _manager.ForEach(ShiftZoneAdd);
                _manager.Sync();
            }

            if (_mode == _removeMode)
            {
                _manager.ForEach(ShiftZoneRemove);
                ColorManager.ChangeColor((WBObjectId)_pivotZone.ObjectId, 6);
                _manager.Remove(_pivotZone);
            }
            
            return true;

        }

        private void ShiftZoneAdd(Zone zone)
        {
            if(zone.System == _pivotZone.System && zone.ZoneNumber >= _pivotZone.ZoneNumber)
            {
                int newZoneNum = zone.ZoneNumber + 1;
                zone.ZoneId = newZoneNum.ToString() + zone.System;
                ColorManager.ChangeColor((WBObjectId)zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));
                
            }
        }

        private void ShiftZoneRemove(Zone zone)
        {
            if (zone.System == _pivotZone.System && zone.ZoneNumber > _pivotZone.ZoneNumber)
            {
                int newZoneNum = zone.ZoneNumber - 1;
                zone.ZoneId = newZoneNum.ToString() + zone.System;
                ColorManager.ChangeColor((WBObjectId)zone.ObjectId, ColorManager.GetColorForZone(zone.ZoneNumber));
            }
        }

        private bool GetPivotZone()
        {
            WBObjectId selectedPolyLine = PolyLineInputRetriever.GetUserInput("\nSelect PolyLine to check for Zone Info: ");
            if (selectedPolyLine.IsNull()) return false;

            
            _pivotZone = _manager.Contains(selectedPolyLine);
            if (_pivotZone == null)
            {
                Active.WriteMessage("\n Is not a Zone!");
                return false;
            }
            return true;
        }

        private bool GetMode()
        {             
            _mode = KeywordInputRetriever.GetUserInput("\nAdding or Removing Zone(s) ", _addMode, _removeMode);
            if (_mode == "-99999") return false;

            return true;
        }
    }
}
