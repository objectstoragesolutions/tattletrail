using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Test.MonitorsControllerTests {
    public class Builder {
        private ILogger<MonitorsController> _logger = Mock.Of<ILogger<MonitorsController>>();
        private IMonitorModelFactory _factoryModel = Mock.Of<IMonitorModelFactory>();
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

        public Builder WithMonitorRepository(IMonitorRepository<MonitorProcess> monitorRepository) {
            _monitorRepository = monitorRepository;
            return this;
        }

        public Builder WithCheckInRepository(ICheckInRepository<CheckIn> checkInRepository) {
            _checkInRepository = checkInRepository;
            return this;
        }

        public MonitorsController Build() {
            return new MonitorsController(_logger, _factoryModel, _monitorRepository, _checkInRepository);
        }
    }
}
