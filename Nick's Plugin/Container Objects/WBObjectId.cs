using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;

namespace WBPlugin
{
    public class WBObjectId : IEquatable<IWBObjectId>, IWBObjectId
    {
        private readonly long _handle;

        public WBObjectId(long handle)
        {
            this._handle = handle;
        }

        public WBObjectId(ObjectId id)
        {
            this._handle = id.Handle.Value;
        }

        public long Handle { get => _handle; }

        public bool Equals(IWBObjectId other)
        {
            return this.Handle.Equals(other.Handle);
        }

        public bool IsNull()
        {
            return _handle == 0;
        }

        public ObjectId GetId()
        {
            return Active.Database.GetObjectId(false, new Handle(this.Handle), 0);
        }


    }
}
