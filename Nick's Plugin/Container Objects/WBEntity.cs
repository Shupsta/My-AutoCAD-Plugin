using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin
{
    public class WBEntity
    {
        public WBObjectId ObjectId { get; private set; }
        public string TypeName { get; set; }
        public string Layer { get; set; }
        
        public WBEntity(WBObjectId id)
        {
            ObjectId = id;
        }

        public bool IsNull()
        {
            return this.ObjectId.IsNull();
        }
    }
}
