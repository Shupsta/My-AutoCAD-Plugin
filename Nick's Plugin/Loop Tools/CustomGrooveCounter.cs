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
        private readonly string _customRouteScheduleBlock = "custom route count";
        private readonly string[] _customGrooveLayers = { };


        public void Count(PointInputRetriever pointRetriever)
        {
            
            Editor ed = Active.Editor;
            
            if (!BlockFinder.FindBlock(_customRouteScheduleBlock)) return;

            int currentSpace = GetCurrentSpace();

            if (currentSpace == 2) //paperspace
            {
                ed.WriteMessage("\nSwitching to Modelspace...");
                ed.SwitchToModelSpace();
            }

            CustomeGrooveReport report = new CustomeGrooveReport(GetCustomGrooveEntities());

            currentSpace = GetCurrentSpace();

            if (currentSpace == 1) //floating modelspace
            {
                ed.WriteMessage("\nSwitching to Paperspace...");
                ed.SwitchToPaperSpace();
            }

            Active.WriteMessage("\nNumber of turns = " + report.ArcCount);
            Active.WriteMessage("\nTotal straight length = " + report.LineLengths);

            ObjectId id = InsertCustomGrooveSchedule(pointRetriever);
            FillAttributes(id, report);
        }

        

        private int GetCurrentSpace()
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

        private ObjectId[] GetCustomGrooveEntities()
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

        private ObjectId InsertCustomGrooveSchedule(PointInputRetriever pointRetriever)
        {
            WBPoint3d insertPoint = pointRetriever.GetUserInput("\nInsertion Point :", false);
            if (insertPoint.IsNull()) return ObjectId.Null;

            WBLayerTableRecord ltr = new WBLayerTableRecord("B_Schedules");
            if (ltr.IsNull()) return ObjectId.Null;

            ObjectId blockId = BlockInsert.Insert(_customRouteScheduleBlock, insertPoint, ltr, 0);
            if (blockId.IsNull) return ObjectId.Null;

            return blockId;
        }

        private void FillAttributes(ObjectId id, CustomeGrooveReport report)
        {
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                BlockReference br = tr.GetObject(id, OpenMode.ForRead, true) as BlockReference;
                AttributeCollection attCol = br.AttributeCollection;

                foreach (ObjectId attId in attCol)
                {
                    AttributeReference attRef = (AttributeReference)tr.GetObject(attId, OpenMode.ForWrite);
                    switch (attRef.Tag)
                    {
                        case "Turns:": attRef.TextString = report.ArcCount.ToString(); break;
                        case "Length:": attRef.TextString = report.LineLengths.ToString()+"'"; break;
                        default: break;
                    }
                }
                tr.Commit();
            }
        }
    }

    class CustomeGrooveReport
    {
        private double _arcLength;
        private double _lineLength;
        private int _arcCount;
        private int _lineCount;
        public double ArcLengths { get => _arcLength;}
        public double LineLengths { get => _lineLength/12;}
        public int ArcCount { get => _arcCount;}
        public int LineCount { get => _lineCount;}
        
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
                            _arcLength += arc.Length;
                            _arcCount += 1;
                            break;
                        case "Line":
                            Line line = (Line)ent;
                            _lineLength += line.Length;
                            _lineCount += 1;
                            break;
                    }
                }
            }
        }
    }
}
