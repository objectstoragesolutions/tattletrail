using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class CheckInModelFactory : ICheckInModelFactory {
        public CheckIn Create(Guid monitorId) {
            return new CheckIn { CheckInId = Guid.NewGuid(), MonitorId = monitorId };
        }
    }
}
