using RightpointLabs.Tracker.Domain.Models;

namespace RightpointLabs.Tracker.Domain.Repositories
{
    public interface IDeviceSnapshotRepository
    {
        void Add(DeviceSnapshot entity);
    }
}