using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.MonitorStatusNotifyer {
    public class MonitorStatusNotifyer : IMonitorStatusNotifyer {

        private readonly IMonitorRepository<MonitorProcess> _monitorRepository;
        private readonly ICheckInRepository<CheckIn> _checkInRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<MonitorStatusNotifyer> _logger;

        public MonitorStatusNotifyer(IMonitorRepository<MonitorProcess> monitorRepository, ICheckInRepository<CheckIn> checkInRepository, IEmailService emailService, ILogger<MonitorStatusNotifyer> logger) {
            _monitorRepository = monitorRepository ?? throw new ArgumentNullException(nameof(IMonitorRepository<MonitorProcess>));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(ICheckInRepository<CheckIn>));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(EmailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorStatusNotifyer));
        }

        public async Task NotifyAsync() {
            try {
                var allMonitors = await _monitorRepository.GetAllAsync();

                var allMonitorIds = allMonitors.Select(x => x.Id);

                var allCheckIns = await _checkInRepository.GetAllAsync();

                var checkInedMonitorIds = allCheckIns.Select(x => x.MonitorId);

                var noCheckinsFor = allMonitorIds.Except(checkInedMonitorIds);

                foreach (var monitorId in noCheckinsFor) {
                    var problematicMonitor = allMonitors.Where(x => x.Id.Equals(monitorId)).First();

                    if (problematicMonitor != null) {

                        problematicMonitor.MonitorDetails.IsDown = true;

                        await _monitorRepository.CreateAsync(problematicMonitor);

                        Console.WriteLine($"No checkIns for {problematicMonitor.MonitorDetails.ProcessName} with id {problematicMonitor.Id}");
                        //await _emailService.SendEmailAsync(monitor.MonitorDetails.Subscribers, "Cant find check in for process", "Looks like your process goes off");
                    }
                }
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorStatusNotifyer: {ex.Message}");
            }
        }
    }
}
