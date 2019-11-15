using System;
using System.Threading;
using System.Threading.Tasks;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.MonitorsStatusService.HostedService;
using TattleTrail.Infrastructure.MonitorStatusNotifyer;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.MonitorsStatusService {
    public class MonitorsStatusService: TimedHostedService {

        private readonly IMonitorRepository<MonitorProcess> _monitorRepository;
        private readonly ICheckInRepository<CheckIn> _checkInRepository;
        private readonly IMonitorStatusNotifyer _monitorStatusNotifyer;

        public MonitorsStatusService(IMonitorRepository<MonitorProcess> monitorRepository, ICheckInRepository<CheckIn> checkInRepository, IMonitorStatusNotifyer monitorStatusNotifyer) {
            _monitorRepository = monitorRepository ?? throw new ArgumentNullException(nameof(IMonitorRepository<MonitorProcess>));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(ICheckInRepository<CheckIn>));
            _monitorStatusNotifyer = monitorStatusNotifyer ?? throw new ArgumentNullException(nameof(MonitorStatusNotifyer));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken) {
            while (!cancellationToken.IsCancellationRequested) {

                await _monitorStatusNotifyer.NotifyAsync();

                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }
    }
}
