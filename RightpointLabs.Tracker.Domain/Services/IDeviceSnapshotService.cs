using System.Threading.Tasks;
using RightpointLabs.Tracker.Domain.Models;

namespace RightpointLabs.Tracker.Domain.Services
{
    public interface IDeviceSnapshotService
    {
        Task<DeviceSnapshot> TakeSnapshot();
    }
}