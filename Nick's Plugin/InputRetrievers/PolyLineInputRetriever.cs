using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin
{
    public class PolyLineInputRetriever
    {

        public static WBObjectId GetUserInput(string prompt)
        {
            Editor ed = Active.Editor;

            TypedValue[] values = new TypedValue[1] { new TypedValue((int)DxfCode.Start, "LWPOLYLINE") };//only select plines
            SelectionFilter filter = new SelectionFilter(values);
            PromptSelectionOptions opts = new PromptSelectionOptions
            {
                MessageForAdding = prompt,
                SingleOnly = true
            };
            PromptSelectionResult psr = ed.GetSelection(opts, filter);
            if (psr.Status != PromptStatus.OK || psr.Status == PromptStatus.None) return new WBObjectId(0); //check for esc
            return new WBObjectId(psr.Value.GetObjectIds()[0].Handle.Value);
        }
        
    }
}
