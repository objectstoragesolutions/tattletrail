using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IMonitorModelFactory {

        private readonly IMonitorDetailsFactory _monitorDetailsFactory;

        public MonitorModelFactory(IMonitorDetailsFactory factory) {
            _monitorDetailsFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public MonitorProcess Create(MonitorDetails monitorDetails) {
            return new MonitorProcess() { 
                Id = Guid.NewGuid(),
                MonitorDetails = _monitorDetailsFactory.Create(monitorDetails)
            };
        }

        public MonitorProcess Create(Guid id, MonitorDetails monitorDetails) {
            return new MonitorProcess() {
                Id = id,
                MonitorDetails = _monitorDetailsFactory.Create(monitorDetails)
            };
        }
    }
}
