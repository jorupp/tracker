using RightpointLabs.Tracker.Domain.Models;
using RightpointLabs.Tracker.Domain.Repositories;
using RightpointLabs.Tracker.Infrastructure.Persistence.Collections;

namespace RightpointLabs.Tracker.Infrastructure.Persistence.Repositories
{
    public class DeviceSnapshotRepository : EntityRepository<DeviceSnapshot>, IDeviceSnapshotRepository
    {
        public DeviceSnapshotRepository(EntityCollectionDefinition<DeviceSnapshot> collectionDefinition) : base(collectionDefinition)
        {
        }
    }
}
