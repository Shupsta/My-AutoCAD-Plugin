using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities
{
    public static class LayerGenerator
    {
        public static LayerTableRecord CreateOrGetLayer(string layername)
        {
            short color = 7;
            LineWeight lWeight = LineWeight.LineWeight000;

            LayerTableRecord ltr = CreateOrGetLayer(layername, color, lWeight);
            return ltr;
        }

        public static LayerTableRecord CreateOrGetLayer(String layername, short color, LineWeight lWeight)
        {
            Database db = Active.Database;

            using (DocumentLock dimlock = Active.Document.LockDocument())
            {
                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    try
                    {
                        LayerTable lt = (LayerTable)tr.GetObject(db.LayerTableId, OpenMode.ForWrite);

                        if (layername == "*Current*") //layer is current
                            return (LayerTableRecord)tr.GetObject(db.Clayer, OpenMode.ForRead);
                        else if (lt.Has(layername)) //layer already exists
                            return (LayerTableRecord)tr.GetObject(lt[layername], OpenMode.ForRead, true);
                        else //create the layer
                        {
                            
                            LayerTableRecord ltr = CreateLayer(layername, color, lWeight);
                            AddNewLayerToLayerTable(lt, ltr);
                            
                            tr.AddNewlyCreatedDBObject(ltr, true);
                            tr.Commit();
                            return ltr;
                        }
                    }
                    catch (Autodesk.AutoCAD.Runtime.Exception ex)
                    {
                        tr.Abort();
                        Active.WriteMessage("\nProblem occured because " + ex.Message.ToString());
                        return null;
                    }
                }
            }
        }

        private static void AddNewLayerToLayerTable(LayerTable lt, LayerTableRecord ltr)
        {
            lt.UpgradeOpen();
            lt.Add(ltr);
        }
        
        private static LayerTableRecord CreateLayer(String layerName, short color, LineWeight lWeight)
        {
            
            LayerTableRecord ltr = new LayerTableRecord() 
            { Name = layerName, 
            Color = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByAci, color),
            LineWeight = lWeight
            };

            return ltr;
        }
    }
}
