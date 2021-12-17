using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public class WBObjectIdCollection
    {
        private readonly ObjectIdCollection _idCollection;

        public WBObjectIdCollection()
        {
            _idCollection = null;
        }

        public WBObjectIdCollection(ObjectIdCollection incomingCollection)
        {
            _idCollection = incomingCollection;
        }

        public bool IsNull()
        {
            if (_idCollection != null)
                return _idCollection.Count == 0;
            else
                return true;
        }

        public ObjectIdCollection IdCollection { get => _idCollection; }

        public static implicit operator ObjectIdCollection(WBObjectIdCollection obj) => obj.IdCollection;
    }
}
