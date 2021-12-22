using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WBPlugin.Utilities;

namespace WBPlugin.Zone_Tools
{
    public class Zone : IEquatable<WBObjectId>
    {
        private WBObjectId _objectId;
        private int _zoneNumber;
        private string _thermostat;
        private string _system;

        public Zone(WBObjectId objectId, string zoneId)
        {
            _objectId = objectId;
            SetNumberAndSystem(zoneId);
            _thermostat = "H";

        }

        public Zone(WBObjectId objectId, int zoneNum, string system, string thermostat)
        {
            _objectId = objectId;
            _zoneNumber = zoneNum;
            _system = system;
            _thermostat = thermostat;
        }

        public int ZoneNumber { get => _zoneNumber; }
        public string System { get => _system; }
        public string ZoneId { 
            get {
                if (System == "") return ZoneNumber.ToString();
                else return ZoneNumber.ToString() + System;
                }
        }

        public WBObjectId ObjectId { get => _objectId; }

        public int Color { get { return ColorManager.GetColor(this.ObjectId); } }

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

        public bool Equals(WBObjectId other)
        {
            return this.ObjectId.Equals(other);
        }

        private void SetNumberAndSystem(string zoneId)
        {
            _zoneNumber = Convert.ToInt32(Regex.Match(zoneId, @"\d+").Value);
            _system = Regex.Match(zoneId, @"\D").Value;

        }
    }
}
