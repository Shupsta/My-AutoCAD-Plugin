using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using WBPlugin.Utilities;

namespace WBPlugin.Tube_Tools
{
    public class TubeManager
    {
        private string[] _tubeLayer = new string[] { "TUBING", "BURIED TUBING", "CUSTOM GROOVE", "T_TUBING", "T_BURIED TUBING", "T_CUSTOM GROOVE", "T_SUPPLY" };
        private string[] _tubeEntityTypes = new string[] { "ARC", "LINE", "LWPOLYLINE" };
        private string JoinedTubeEntityTypes { get => String.Join(",", _tubeEntityTypes); }
        private string JoinedTubeLayers { get => String.Join(",", _tubeLayer); }
        
        internal bool IsTube(WBEntity tubeEntity)
        {
            if (tubeEntity.TypeName == "LINE" || tubeEntity.TypeName == "ARC" || tubeEntity.TypeName == "POLYLINE")
            {
                foreach (string layer in _tubeLayer)
                {
                    if (tubeEntity.Layer == layer)
                        return true;
                }
            }            
            Active.WriteMessage("\nInvalid Selection. Not a line, arc, or polyline.");
            return false;
        }

        

        public static Tuple<WBPoint3d, WBPoint3d> GetStartOrEndPoint(WBEntity tube)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId id = tube.ObjectId.GetId();
                Curve ent = (Curve)id.GetObject(OpenMode.ForRead, false);
                return new Tuple<WBPoint3d, WBPoint3d>(
                    new WBPoint3d(ent.StartPoint.X, ent.StartPoint.Y), 
                    new WBPoint3d(ent.EndPoint.X, ent.EndPoint.Y));              
            }
        }

        public static double GetLength(WBEntity entity)
        {
            double length = 0;

            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId id = entity.ObjectId.GetId();

                switch (entity.TypeName)
                {
                    case "LINE":
                        Line line = (Line)id.GetObject(OpenMode.ForRead, false);
                        length = line.Length;
                        break;
                    case "ARC":
                        Arc arc = (Arc)id.GetObject(OpenMode.ForRead, false);
                        length = arc.Length;
                        break;
                    case "POLYLINE":
                        Polyline polyline = (Polyline)id.GetObject(OpenMode.ForRead, false);
                        length = polyline.Length;
                        break;
                    default:
                        length = 0;
                        break;

                }
                return length;
            }
            
            
            
            
        }

        internal List<Tube> GetAllTubes()
        {
            Document doc = Active.Document;
            Editor ed = Active.Editor;
            List<Tube> tubes = new List<Tube>();

            using (DocumentLock docLock = doc.LockDocument())
            using(Transaction tr = doc.TransactionManager.StartTransaction())
            {
                try
                {
                    // Find all entities of a given type and layer
                    TypedValue[] values = { new TypedValue((int)DxfCode.Start, JoinedTubeEntityTypes),
                                new TypedValue((int)DxfCode.LayerName, JoinedTubeLayers) };
                    SelectionFilter sFilter = new SelectionFilter(values);

                    PromptSelectionResult psr = ed.SelectAll(sFilter);
                    if (psr.Status != PromptStatus.OK)
                    {
                        ed.WriteMessage("\nNo TUBES exist in drawing.");
                        return null;
                    }

                    // Build an array of all the tube items in the drawing
                    SelectionSet ss = psr.Value;
                    ObjectId[] idArray = ss.GetObjectIds();

                    foreach(ObjectId oID in idArray)
                    {
                        WBObjectId id = new WBObjectId(oID.Handle.Value);
                        WBEntity ent = new WBEntity(id);
                        Tube tube = new Tube(ent);
                        tubes.Add(tube);
                    }
                    
                    tr.Commit();
                    return tubes;
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    tr.Abort();
                    ed.WriteMessage("\nProblem occured in TubeManager.GetAllTubes() because " + ex.Message.ToString());
                    return null;
                }
            }
        }
    }
}
