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
        public static List<long> Encode(ObjectIdCollection idCollection)
        {
            List<long> encodedList = new List<long>();
            foreach(ObjectId id in idCollection)
            {
                encodedList.Add(id.Handle.Value);
            }
            return encodedList;
        }

        public static ObjectIdCollection Decode(List<long> encodedList)
        {
            ObjectIdCollection decodedList = new ObjectIdCollection();
            foreach(long value in encodedList)
            {
                decodedList.Add(Active.Database.GetObjectId(false, new Handle(value), 0));
            }
            return decodedList;
        }
    }
}
