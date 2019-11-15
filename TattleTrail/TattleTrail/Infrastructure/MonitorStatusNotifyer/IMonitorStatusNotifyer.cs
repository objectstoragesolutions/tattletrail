using System.Threading.Tasks;

namespace TattleTrail.Infrastructure.MonitorStatusNotifyer {
    public interface IMonitorStatusNotifyer {
        Task NotifyAsync();
    }
}