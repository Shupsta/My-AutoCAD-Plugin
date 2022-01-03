using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities
{
    public class BlockIdentifier
    {
        public static bool IsBlockType(BlockReference block, string blockName)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(Active.Database.BlockTableId, OpenMode.ForRead, false) as BlockTable;

                if (!bt.Has(blockName)) return false;

                if (block.IsDynamicBlock)
                {
                    //BlockTableRecord btr = tr.GetObject(bt[blockName], OpenMode.ForRead, false) as BlockTableRecord;
                    if (block.DynamicBlockTableRecord == bt[blockName]) return true;
                }
                else
                {
                    if (block.BlockTableRecord == bt[blockName]) return true;
                }
            }

            return false;
        }
    }
}
