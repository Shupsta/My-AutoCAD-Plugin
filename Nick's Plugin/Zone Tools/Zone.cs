using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public class Zone
    {
        private WBObjectId _objectId;
        private string _zoneId;
        private string _thermostat;

        public Zone(WBObjectId objectId, string zoneId)
        {
            _objectId = objectId;
            _zoneId = zoneId;
            _thermostat = "H";

        }

        public string ZoneNumber { get => _zoneId; }

        public WBObjectId ObjectId { get => _objectId; }

        public int Color { get { return ColorChanger.GetColorForZone(this.ZoneNumber); } }

        public string Thermostat { get => _thermostat;
            set {

                switch (value)
                {
                    case "H":
                        _thermostat = value;
                        break;
                    case "W":
                        _thermostat = value;
                        break;
                    case "C":
                        _thermostat = value;
                        break;
                    default:
                        _thermostat = "Unknown";
                        break;
                }

            }  
        }
    }
}
