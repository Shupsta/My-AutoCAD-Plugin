using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class WBObjectId
    {
        private long _handle;
        
        public WBObjectId(long handle)
        {
            this._handle = handle;
        }

        public long Handle { get => _handle; }

    }
}
