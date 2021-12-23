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

        public static WBPoint3d GetStartOrEndPoint(WBEntity tube, string mode)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId id = ObjectIdTranslator.Decode(tube.ObjectId);
                Curve ent = (Curve)id.GetObject(OpenMode.ForRead, false);
                if(mode.Equals("start"))
                    return new WBPoint3d(ent.StartPoint.X, ent.StartPoint.Y);
                if (mode.Equals("end"))
                    return new WBPoint3d(ent.EndPoint.X, ent.EndPoint.Y);
                else
                    return new WBPoint3d();
            }
        }

        public static double GetLength(WBEntity entity)
        {
            double length = 0;

            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                ObjectId id = ObjectIdTranslator.Decode(entity);

                switch (entity.TypeName)
                {
                    case "Line":
                        Line line = (Line)id.GetObject(OpenMode.ForRead, false);
                        length = line.Length;
                        break;
                    case "Arc":
                        Arc arc = (Arc)id.GetObject(OpenMode.ForRead, false);
                        length = arc.Length;
                        break;
                    case "Polyline":
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

        
    }
}
