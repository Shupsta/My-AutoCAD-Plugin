using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class IntegerInputRetriever
    {
        public static int GetUserInput(string prompt, int defaultValue)
        {
            return GetUserInput(prompt, defaultValue, 0);
        }

        public static int GetUserInput(string prompt, int defaultValue, int upperLimit)
        {
            Editor ed = Active.Editor;

            PromptIntegerOptions pio = new PromptIntegerOptions(prompt);
            pio.DefaultValue = defaultValue;
            pio.AllowNone = true;
            pio.AllowNegative = false;

            if (upperLimit != 0) pio.UpperLimit = upperLimit;

            PromptIntegerResult pir = ed.GetInteger(pio);
            if (pir.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\n*Cancel*");
                return 0;
            }

            if (pir.Status == PromptStatus.None)
            {
                return defaultValue;
            }

            return pir.Value;
        }


    }
}
