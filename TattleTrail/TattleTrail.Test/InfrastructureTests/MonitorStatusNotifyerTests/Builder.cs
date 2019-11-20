using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.MonitorStatusNotifyer;
using TattleTrail.Models;

namespace TattleTrail.Test.InfrastructureTests.MonitorStatusNotifyerTests {
    public class Builder {
        private IMonitorRepository<MonitorProcess> _monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>();
        private ICheckInRepository<CheckIn> _checkInRepository = Mock.Of<ICheckInRepository<CheckIn>>();
        private IEmailService _emailService = Mock.Of<IEmailService>();
        private ILogger<MonitorStatusNotifyer> _logger = Mock.Of<ILogger<MonitorStatusNotifyer>>();

        public Builder WithMonitorRepository(IMonitorRepository<MonitorProcess> monitorRepository) {
            _monitorRepository = monitorRepository;
            return this;
        }

        public Builder WithCheckInRepository(ICheckInRepository<CheckIn> checkInRepository) {
            _checkInRepository = checkInRepository;
            return this;
        }

        public Builder WithEmailService(IEmailService emailService) {
            _emailService = emailService;
            return this;
        }

        public Builder WithLogger(ILogger<MonitorStatusNotifyer> logger) {
            _logger = logger;
            return this;
        }

        public MonitorStatusNotifyer Build() {
            return new MonitorStatusNotifyer(_monitorRepository, _checkInRepository, _emailService, _logger);
        }
    }
}
