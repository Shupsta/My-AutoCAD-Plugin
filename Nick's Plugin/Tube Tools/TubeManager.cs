using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using WBPlugin.Utilities;

namespace WBPlugin.Tube_Tools
{
    public class TubeManager
    {
        private string[] _tubeLayer = new string[] { "TUBING", "BURIED TUBING", "CUSTOM GROOVE", "T_TUBING", "T_BURIED TUBING", "T_CUSTOM GROOVE", "T_SUPPLY" };
        
        internal bool IsTube(WBEntity tubeEntity)
        {
            if (tubeEntity.TypeName != "Line" && tubeEntity.TypeName != "Arc" && tubeEntity.TypeName != "Polyline")
            {
                Active.WriteMessage("\nInvalid Selection. Not a line, arc, or polyline.");
            }

            foreach (string layer in _tubeLayer)
            {
                if (tubeEntity.Layer == layer)
                    return true;
            }

            return false;
        }
    }
}
