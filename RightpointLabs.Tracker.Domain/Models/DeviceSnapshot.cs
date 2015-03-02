using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightpointLabs.Tracker.Domain.Models
{
    public class DeviceSnapshot : Entity
    {
        public DateTime Timestamp { get; set; }
        public DeviceState[] Devices { get; set; }
    }

    public class DeviceState
    {
        public DateTime? Date { get; set; }
        public string AccessPointName { get; set; }
        public int ClientHealth { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set;  }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string VendorDeviceType { get; set; }
        public int Bandwidth { get; set; }
        public int Signal { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
