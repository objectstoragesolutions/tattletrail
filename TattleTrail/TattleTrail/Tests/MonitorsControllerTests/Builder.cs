using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Tests.MonitorsControllerTests {
    public class Builder {
        private ILogger<MonitorsController> _logger = Mock.Of<ILogger<MonitorsController>>();
        private IRepository<MonitorModel> _repository = Mock.Of<IRepository<MonitorModel>>();
        private IBaseModelFactory<MonitorModel> _factoryModel = Mock.Of<IBaseModelFactory<MonitorModel>>();
        public Builder WithLogger(ILogger<MonitorsController> logger) {
            _logger = logger;
            return this;
        }

        public Builder WithRepository(IRepository<MonitorModel> repository) {
            _repository = repository;
            return this;
        }

        public Builder WithModelFactory(IBaseModelFactory<MonitorModel> modelFactory) {
            _factoryModel = modelFactory;
            return this;
        }

        public MonitorsController Build() {
            return new MonitorsController(_logger, _repository, _factoryModel);
        }
    }
}
