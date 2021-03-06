using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using WBPlugin.Utilities;

namespace WBPlugin.Tube_Tools
{
    public class TubeManager
    {
        private static readonly string[] _tubeLayer = new string[] { "TUBING", "BURIED TUBING", "CUSTOM GROOVE", "T_TUBING", "T_BURIED TUBING", "T_CUSTOM GROOVE", "T_SUPPLY" };
        private readonly string[] _tubeEntityTypes = new string[] { "ARC", "LINE", "LWPOLYLINE" };
        private string JoinedTubeEntityTypes { get => String.Join(",", _tubeEntityTypes); }
        private string JoinedTubeLayers { get => String.Join(",", _tubeLayer); }
        
        public static bool IsTube(WBEntity tubeEntity)
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

        

        public static Tuple<WBPoint3d, WBPoint3d, WBPoint3d> GetStartOrEndPoint(WBEntity tube)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId id = tube.ObjectId.GetId();
                Curve ent = (Curve)id.GetObject(OpenMode.ForRead, false);
                LineSegment3d lineSeg = new LineSegment3d(ent.StartPoint, ent.EndPoint);
                return new Tuple<WBPoint3d, WBPoint3d, WBPoint3d>(
                    new WBPoint3d(ent.StartPoint.X, ent.StartPoint.Y), 
                    new WBPoint3d(ent.EndPoint.X, ent.EndPoint.Y),
                    new WBPoint3d(lineSeg.MidPoint.X, lineSeg.MidPoint.Y));              
            }
        }

        public static double GetLength(WBEntity entity)
        {
            double length;

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

        private static double GetLength(Entity entity)
        {
            ObjectId id = entity.ObjectId;
            switch (entity.GetType().Name)
            {
                case "LINE":
                    Line line = (Line)id.GetObject(OpenMode.ForRead, false);
                    return line.Length;
                case "ARC":
                    Arc arc = (Arc)id.GetObject(OpenMode.ForRead, false);
                    return arc.Length;
                case "POLYLINE":
                    Polyline polyline = (Polyline)id.GetObject(OpenMode.ForRead, false);
                    return polyline.Length;
                default:
                    return 0;

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
                        Entity entity = oID.GetObject(OpenMode.ForRead, false) as Entity;
                        Tube tube = new Tube(ent, GetLength(entity));
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
        /// <summary>
        /// Can return null
        /// </summary>
        /// <returns>A tube object which represents an object in CAD</returns>
        public static Tube GetTube()
        {
            WBEntity entity = EntityInputRetriever.GetUserInput("\nSelect a tube entity");
            if (entity.IsNull()) return null;

            if (!TubeManager.IsTube(entity)) return null;

            return new Tube(entity);
        }
    }
}
