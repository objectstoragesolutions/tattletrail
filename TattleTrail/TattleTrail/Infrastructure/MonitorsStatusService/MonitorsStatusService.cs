using System;
using System.Threading;
using System.Threading.Tasks;
using TattleTrail.Infrastructure.MonitorsStatusService.HostedService;
using TattleTrail.Infrastructure.MonitorStatusNotifyer;

namespace TattleTrail.Infrastructure.MonitorsStatusService {
    public class MonitorsStatusService: TimedHostedService {
        private readonly IMonitorStatusNotifyer _monitorStatusNotifyer;

        public MonitorsStatusService(IMonitorStatusNotifyer monitorStatusNotifyer) {
            _monitorStatusNotifyer = monitorStatusNotifyer ?? throw new ArgumentNullException(nameof(IMonitorStatusNotifyer));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {

                await _monitorStatusNotifyer.NotifyAsync();

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
