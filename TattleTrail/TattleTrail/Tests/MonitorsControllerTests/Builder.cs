using Microsoft.Extensions.Logging;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    public class Builder {
        private ILogger<MonitorsController> _logger;
        private IRepository<MonitorModel> _repository;
        public MonitorsController WithLogger(ILogger<MonitorsController> logger) {
            _logger = logger;
            return Build();
        }

        public MonitorsController WithRepository(IRepository<MonitorModel> repository) {
            _repository = repository;
            return Build();
        }

        public MonitorsController Build() {
            return new MonitorsController(_logger, _repository);
        }
    }
}
