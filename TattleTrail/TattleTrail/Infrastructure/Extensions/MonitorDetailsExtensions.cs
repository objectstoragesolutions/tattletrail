using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class MonitorDetailsExtensions {
        public static MonitorDetails UpdateMonitorDetails(this MonitorDetails monitorForUpdate, MonitorDetails newDetails) {
            monitorForUpdate.Subscribers = newDetails.Subscribers;
            monitorForUpdate.IntervalTime = newDetails.IntervalTime;
            monitorForUpdate.ProcessName = newDetails.ProcessName;
            return monitorForUpdate;
        }
    }
}
