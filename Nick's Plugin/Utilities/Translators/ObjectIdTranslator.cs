using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin.Utilities
{
    public static class ObjectIdTranslator
    {
        public static List<WBObjectId> Encode(ObjectIdCollection idCollection)
        {
            List<WBObjectId> encodedList = new List<WBObjectId>();
            foreach(ObjectId id in idCollection)
            {
                encodedList.Add(new WBObjectId(id.Handle.Value));
            }
            return encodedList;
        }

        public static ObjectIdCollection Decode(List<WBObjectId> encodedList)
        {
            ObjectIdCollection decodedList = new ObjectIdCollection();
            foreach(WBObjectId value in encodedList)
            {
                decodedList.Add(Active.Database.GetObjectId(false, new Handle(value.Handle), 0));
            }
            return decodedList;
        }
    }
}
