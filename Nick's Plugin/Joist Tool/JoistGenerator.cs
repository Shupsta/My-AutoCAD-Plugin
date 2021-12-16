using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin
{
    public class JoistGenerator
    {
        private Transaction _tr;
        private BlockTableRecord _btr;
        private Editor _ed;
        private Joists _joists;
        
        public void RunJoists(Joists joistInfo)
        {
            _joists = joistInfo;
            Editor ed = Active.Editor;
            _ed = ed;
            Database db = Active.Database;

            using(DocumentLock docLock = Active.Document.LockDocument())
            using(Transaction tr = db.TransactionManager.StartTransaction())
            {
                _tr = tr;
                
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                _btr = btr;
                                
                Hatch hatch = CreateHatchObj();
                                
                AddOuterBoundary(hatch);
                                
                AddInteriorBoundaries(hatch);

                hatch.EvaluateHatch(true);

                ExplodeHatch(hatch);

                hatch.Erase(); 
                tr.Commit();
                btr.Dispose();
            }
            
        }

        private Hatch CreateHatchObj()
        {
            Hatch hatch = new Hatch();
            _btr.AppendEntity(hatch);
            _tr.AddNewlyCreatedDBObject(hatch, true);

            hatch.SetDatabaseDefaults();
            hatch.SetHatchPattern(HatchPatternType.PreDefined, "ANSI31");
            hatch.PatternAngle = (_joists.Angle - (45 * System.Math.PI / 180));
            hatch.PatternScale = _joists.JoistSpacing * 8;
            hatch.Layer = _joists.JoistLayer;
            Point2d hatchOrigin = new Point2d(_joists.StartPoint.X, _joists.StartPoint.Y);
            hatch.Origin = hatchOrigin;
            hatch.SetHatchPattern(hatch.PatternType, hatch.PatternName);
            hatch.Associative = true;

            return hatch;
        }

        private void AddOuterBoundary(Hatch hatch)
        {
            try
            {
                hatch.AppendLoop(HatchLoopTypes.Outermost, _joists.OuterBoundary.IdCollection);
            }
            catch (Autodesk.AutoCAD.Runtime.Exception ex)
            {
                _tr.Abort();
                _ed.WriteMessage("\nProblem occured because " + ex.Message.ToString());
                _ed.WriteMessage("\nProblem with outer boundary.  Is it closed?");
                _btr.Dispose();
                return;
            }
        }

        private void AddInteriorBoundaries(Hatch hatch)
        {
            foreach (ObjectIdCollection interiorBndy in _joists.InteriorBoundaries)
            {
                try
                {
                    if (interiorBndy != null)
                    {
                        hatch.AppendLoop(HatchLoopTypes.Default, interiorBndy);
                    }
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    _tr.Abort();
                    _ed.WriteMessage("\nProblem occured because " + ex.Message.ToString());
                    _ed.WriteMessage("\nProblem with interior boundary. Is it closed?");
                    _btr.Dispose();
                    return;
                }
            }
        }

        private void ExplodeHatch(Hatch hatch)
        {
            DBObjectCollection hatchCollection = new DBObjectCollection();
            hatch.Explode(hatchCollection);

            foreach (Entity ent in hatchCollection)
            {
                ent.Linetype = "ByLayer";
                _btr.AppendEntity(ent);
                _tr.AddNewlyCreatedDBObject(ent, true);
            }
        }
    }
}
