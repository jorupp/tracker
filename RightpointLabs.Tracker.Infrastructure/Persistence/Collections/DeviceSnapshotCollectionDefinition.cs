using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RightpointLabs.Tracker.Domain.Models;

namespace RightpointLabs.Tracker.Infrastructure.Persistence.Collections
{
    public class DeviceSnapshotCollectionDefinition : EntityCollectionDefinition<DeviceSnapshot>
    {
        public DeviceSnapshotCollectionDefinition(IMongoConnectionHandler connectionHandler) : base(connectionHandler)
        {
        }
    }
}
