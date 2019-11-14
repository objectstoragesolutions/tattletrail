using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Tests.MonitorsControllerTests {
    public class Builder {
        private ILogger<MonitorsController> _logger = Mock.Of<ILogger<MonitorsController>>();
        private IMonitorModelFactory _factoryModel = Mock.Of<IMonitorModelFactory>();
        private IEmailService _emailService = Mock.Of<IEmailService>();
        private IMonitorRepository<MonitorProcess> _monitorRepository = Mock.Of<IMonitorRepository<MonitorProcess>>();
        private ICheckInRepository<CheckIn> _checkInRepository = Mock.Of<ICheckInRepository<CheckIn>>();
        public Builder WithLogger(ILogger<MonitorsController> logger) {
            _logger = logger;
            return this;
        }

        public Builder WithModelFactory(IMonitorModelFactory modelFactory) {
            _factoryModel = modelFactory;
            return this;
        }

        public Builder WithEmailService(IEmailService emailService) {
            _emailService = emailService;
            return this;
        }

        public Builder WithMonitorRepository(IMonitorRepository<MonitorProcess> monitorRepository) {
            _monitorRepository = monitorRepository;
            return this;
        }

        public Builder WithCheckInRepository(ICheckInRepository<CheckIn> checkInRepository) {
            _checkInRepository = checkInRepository;
            return this;
        }

        public MonitorsController Build() {
            return new MonitorsController(_logger, _factoryModel, _emailService, _monitorRepository, _checkInRepository);
        }
    }
}
