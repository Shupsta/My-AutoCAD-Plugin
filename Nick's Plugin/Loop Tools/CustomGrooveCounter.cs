using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace WBPlugin.Loop_Tools
{
    public class CustomGrooveCounter
    {
        public static void Count()
        {
            
        }

        private static int GetCurrentSpace()
        {
            // Get the current values of CVPORT and TILEMODE
            object cvport = Application.GetSystemVariable("CVPORT");
            object tilemode = Application.GetSystemVariable("TILEMODE");

            if (System.Convert.ToInt16(tilemode) == 1)
            {
                //The model layout is active
                return 0;
            }
            else if (System.Convert.ToInt16(tilemode) == 0 && System.Convert.ToInt16(cvport) > 1)
            {
                //floating modelspace
                return 1;
            }
            else //paperspace
                return 2;
        }
    }
}
