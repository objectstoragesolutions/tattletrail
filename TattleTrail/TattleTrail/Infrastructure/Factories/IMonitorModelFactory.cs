using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public interface IMonitorModelFactory {
        MonitorProcess Create(MonitorDetails monitorDetails);

        MonitorProcess Create(Guid id, MonitorDetails monitorDetails);
    }
}