using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPlugin.Zone_Tools
{
    class Zone
    {
        private WBObjectId _objectId;
        private string _zoneId;

        public Zone(WBObjectId objectId, string zoneId)
        {
            _objectId = objectId;
            _zoneId = zoneId;

        }
    }
}
