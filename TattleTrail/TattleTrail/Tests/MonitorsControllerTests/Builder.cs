﻿using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.Factories;

namespace TattleTrail.Tests.MonitorsControllerTests {
    public class Builder {
        private ILogger<MonitorsController> _logger = Mock.Of<ILogger<MonitorsController>>();
        private IRepository _repository = Mock.Of<IRepository>();
        private IMonitorModelFactory _factoryModel = Mock.Of<IMonitorModelFactory>();
        private IEmailService _emailService = Mock.Of<IEmailService>();
        public Builder WithLogger(ILogger<MonitorsController> logger) {
            _logger = logger;
            return this;
        }

        public Builder WithRepository(IRepository repository) {
            _repository = repository;
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

        public MonitorsController Build() {
            return new MonitorsController(_logger, _repository, _factoryModel, _emailService);
        }
    }
}
