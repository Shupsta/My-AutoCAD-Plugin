using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities.ExtensionMethods
{
    public static class BlockReferenceExtensionMethod
    {
        public static void UpdateAttribute(this BlockReference block, string attributeName, string attributeValue)
        {
            using(Transaction tr = Active.Database.TransactionManager.StartTransaction())
            {
                foreach (ObjectId attId in block.AttributeCollection)
                {
                    AttributeReference attribute = tr.GetObject(attId, OpenMode.ForWrite, false) as AttributeReference;
                    if (attribute.Tag.Equals(attributeName))
                    {
                        attribute.TextString = attributeValue;
                    }
                }

                tr.Commit();
            }
            
        }
    }
}
