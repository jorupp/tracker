using RightpointLabs.Tracker.Domain.Models;

namespace RightpointLabs.Tracker.Web.Models
{
    public class HomeIndexModel
    {
        public bool IsRunning { get; set; }
        public DeviceSnapshot LastSnapshot { get; set; }
    }
}