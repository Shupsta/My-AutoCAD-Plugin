using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;

namespace WBPlugin.Utilities.Misc
{
    public static class BlockInsert
    {
        public static ObjectId Insert(string blockName, WBPoint3d insertPoint, WBLayerTableRecord layer, double scale)
        {
            return Insert(blockName, insertPoint, layer, scale, 0);
        }
        public static ObjectId Insert(string blockName, WBPoint3d insertPoint, WBLayerTableRecord layer, double scale, double rotation)
        {
            return Insert(blockName, insertPoint, layer, scale, rotation, Active.Database.CurrentSpaceId);
        }
        public static ObjectId Insert(string blockName, WBPoint3d insertPoint, WBLayerTableRecord layer, double scale, double rotation, ObjectId modelOrPaper)
        {
            ObjectId id = new ObjectId();
            using (Active.Document.LockDocument())
            using (Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                id = Insert(blockName, insertPoint, layer, scale, rotation, modelOrPaper, tr);
                tr.Commit();
            }
            return id;
        }
        public static ObjectId Insert(string blockName, WBPoint3d insertPoint, WBLayerTableRecord layer, double scale, double rotation, ObjectId modelOrPaper, Transaction tr)
        {
            
            using(BlockTable bt = (BlockTable)tr.GetObject(modelOrPaper.Database.BlockTableId, OpenMode.ForRead))
            using(BlockTableRecord btr = (BlockTableRecord)tr.GetObject(modelOrPaper, OpenMode.ForRead))
            {
                ObjectId id = bt.GetId(blockName);
                if (id.IsNull) return ObjectId.Null;

                try
                {
                    BlockReference br = new BlockReference(insertPoint, id);
                    br.SetDatabaseDefaults();
                    br.ScaleFactors = new Scale3d(scale, scale, scale);
                    br.Layer = layer.LayerName;
                    br.Rotation = rotation * System.Math.PI / 180;
                    btr.UpgradeOpen();
                    btr.AppendEntity(br);
                    tr.AddNewlyCreatedDBObject(br, true);

                    BlockTableRecord BlkTblRec = tr.GetObject(id, OpenMode.ForRead) as BlockTableRecord;
                    if (BlkTblRec.HasAttributeDefinitions)
                    {
                        foreach (ObjectId objId in BlkTblRec)
                        {
                            AttributeDefinition AttDef = tr.GetObject(objId, OpenMode.ForRead) as AttributeDefinition;
                            if (AttDef != null)
                            {
                                AttributeReference AttRef = new AttributeReference();
                                AttRef.SetAttributeFromBlock(AttDef, br.BlockTransform);
                                br.AttributeCollection.AppendAttribute(AttRef);
                                tr.AddNewlyCreatedDBObject(AttRef, true);
                            }
                        }
                    }

                    return br.Id;
                }
                catch (Autodesk.AutoCAD.Runtime.Exception ex)
                {
                    tr.Abort();
                    Active.WriteMessage("\nProblem occured because " + ex.Message.ToString());
                }
            }

            return ObjectId.Null;
        }
    }
}
