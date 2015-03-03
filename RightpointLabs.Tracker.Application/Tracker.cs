using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using RightpointLabs.Tracker.Domain.Models;
using RightpointLabs.Tracker.Domain.Repositories;
using RightpointLabs.Tracker.Domain.Services;

namespace RightpointLabs.Tracker.Application
{
    public class Tracker
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IDeviceSnapshotRepository _repository;
        private readonly IDeviceSnapshotService _service;
        private readonly Task _task;
        private volatile DeviceSnapshot _lastDeviceSnapshot = null;

        public Tracker(IDeviceSnapshotRepository repository, IDeviceSnapshotService service)
        {
            _repository = repository;
            _service = service;
            _task = Task.Run(async () => await Execute());
        }

        public bool IsRunning
        {
            get { return !_task.IsCompleted; }
        }

        public DeviceSnapshot LastSnapshot
        {
            get { return _lastDeviceSnapshot; }
        }

        private async Task Execute()
        {
            while (true)
            {
                try
                {
                    await LoadOne();
                }
                catch (Exception ex)
                {
                    log.Warn("LoadOne threw", ex);
                }
                await Task.Delay(TimeSpan.FromSeconds(15));
            }
        }

        private async Task LoadOne()
        {
            var data = await _service.TakeSnapshot();
            _lastDeviceSnapshot = data;
            _repository.Add(data);
        }
    }
}
