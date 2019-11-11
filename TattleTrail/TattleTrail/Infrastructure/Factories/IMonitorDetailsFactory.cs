using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public interface IMonitorDetailsFactory {
        MonitorDetails Create(MonitorDetails details);
    }
}