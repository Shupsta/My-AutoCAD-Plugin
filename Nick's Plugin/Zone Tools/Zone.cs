using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public class Zone : IEquatable<IWBObjectId>
    {
        private IWBObjectId _objectId;
        private int _zoneNumber;
        private string _thermostat;
        private string _system;

        public Zone(IWBObjectId objectId, string zoneId)
        {
            _objectId = objectId;
            ZoneId = zoneId;
            _thermostat = "H";

        }

        public Zone(IWBObjectId objectId, int zoneNum, string system, string thermostat)
        {
            _objectId = objectId;
            _zoneNumber = zoneNum;
            _system = system.ToUpper();
            _thermostat = thermostat;
        }

        public int ZoneNumber { get => _zoneNumber; }
        public string System { get => _system; }
        public string ZoneId {
            get
            {
                if (System == "") return ZoneNumber.ToString();
                else return ZoneNumber.ToString() + System;
            }
            set
            {
                _zoneNumber = Convert.ToInt32(Regex.Match(value, @"\d+").Value);
                _system = Regex.Match(value, @"\D").Value.ToUpper();

            }
        }

        public IWBObjectId ObjectId { get => _objectId; }

        public int Color { get { return ColorManager.GetColor((WBObjectId)this.ObjectId); } }

        public string Thermostat { get => _thermostat;
            set {

                switch (value.ToUpper())
                {
                    case "H":
                        _thermostat = value.ToUpper();
                        break;
                    case "W":
                        _thermostat = value.ToUpper();
                        break;
                    case "C":
                        _thermostat = value.ToUpper();
                        break;
                    default:
                        _thermostat = "Unknown";
                        break;
                }

            }  
        }

        public bool Equals(IWBObjectId other)
        {
            return this.ObjectId.Equals(other);
        }
    }
}
