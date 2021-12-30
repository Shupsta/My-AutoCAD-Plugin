using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using WBPlugin.Utilities.Misc;
using Autodesk.AutoCAD.EditorInput;

namespace WBPlugin.Loop_Tools
{
    public class CustomGrooveCounter
    {
        public static void Count()
        {
            Editor ed = Active.Editor;
            
            if (!BlockFinder.FindBlock("custom route count")) return;

            int currentSpace = GetCurrentSpace();

            if (currentSpace == 2) //paperspace
            {
                ed.WriteMessage("\nSwitching to Modelspace...");
                ed.SwitchToModelSpace();
            }

            CustomeGrooveReport report = new CustomeGrooveReport(GetCustomGrooveEntities());


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

        private static ObjectId[] GetCustomGrooveEntities()
        {
            PromptSelectionOptions pso = new PromptSelectionOptions()
            {
                MessageForAdding = "\nSelect Custom Grooves: ",
                SingleOnly = false
            };

            SelectionFilter filter = new SelectionFilter(
                    new TypedValue[]
                    {
                        new TypedValue((int)DxfCode.Start, "ARC,LINE"),
                        new TypedValue((int)DxfCode.LayerName, "Custom Groove,T_Custom Groove")
                    }
                );

            PromptSelectionResult selectionRes = Active.Editor.GetSelection(pso, filter);

            if(selectionRes.Status != PromptStatus.OK)
            {
                Active.WriteMessage("\n*Cancel*");
            }
            return selectionRes.Value.GetObjectIds();
        }
    }

    class CustomeGrooveReport
    {
        private double _arcLength;
        private double _lineLength;
        private int _arcCount;
        private int _lineCount;
        public double ArcLengths { get => _arcLength; set => _arcLength = value; }
        public double LineLengths { get => _lineLength; set => _lineLength = value; }
        public int ArcCount { get => _arcCount; set => _arcCount = value; }
        public int LineCount { get => _lineCount; set => _lineCount = value; }
        
        public CustomeGrooveReport(ObjectId[] ids)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach(ObjectId id in ids)
                {
                    Entity ent = (Entity)id.GetObject(OpenMode.ForRead, false);

                    switch (ent.GetType().Name)
                    {
                        case "Arc":
                            Arc arc = (Arc)ent;
                            ArcLengths += arc.Length;
                            ArcCount += 1;
                            break;
                        case "Line":
                            Line line = (Line)ent;
                            LineLengths += line.Length;
                            LineCount += 1;
                            break;
                    }
                }
            }
        }
    }
}
