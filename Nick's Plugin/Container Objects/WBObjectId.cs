using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class WBObjectId : IEquatable<WBObjectId>
    {
        private long _handle;
        
        public WBObjectId(long handle)
        {
            this._handle = handle;
        }

        public long Handle { get => _handle; }

        public bool Equals(WBObjectId other)
        {
            return this.Handle.Equals(other.Handle);
        }

        public bool IsNull()
        {
            return _handle == 0;
        }
        
        
    }
}
