using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public class WBObjectIdCollection : IWBObjectIdCollection
    {
        private readonly List<IWBObjectId> _idCollection;

        public WBObjectIdCollection()
        {
            _idCollection = null;
        }

        public WBObjectIdCollection(List<IWBObjectId> incomingCollection)
        {
            _idCollection = incomingCollection;
        }

        public WBObjectIdCollection(ObjectIdCollection idCollection)
        {
            List<IWBObjectId> encodedList = new List<IWBObjectId>();
            foreach (ObjectId id in idCollection)
            {
                encodedList.Add(new WBObjectId(id.Handle.Value));
            }
            _idCollection = encodedList;
        }

        public WBObjectIdCollection(ObjectId[] ids)
        {
            List<IWBObjectId> idCollection = new List<IWBObjectId>();
            foreach(ObjectId id in ids)
            {
                idCollection.Add(new WBObjectId(id.Handle.Value));
            }
            _idCollection = idCollection;
        }

        public bool IsNull()
        {
            if (_idCollection != null)
                return _idCollection.Count == 0;
            else
                return true;
        }

        public void Add(IWBObjectId id)
        {
            _idCollection.Add(id);
        }

        public List<IWBObjectId> IdCollection { get => _idCollection; }

        public ObjectIdCollection GetIdCollection()
        {
            ObjectIdCollection decodedList = new ObjectIdCollection();
            foreach (WBObjectId value in _idCollection)
            {
                decodedList.Add(Active.Database.GetObjectId(false, new Handle(value.Handle), 0));
            }
            return decodedList;
        }


    }
}
