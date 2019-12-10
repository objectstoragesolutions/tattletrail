using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Factories;

namespace TattleTrail.Test.DALTests.RepositoryTests.CheckInRepositoryTests {
    public class Builder {
        private IRedisServerProvider _redisServerProvider = Mock.Of<IRedisServerProvider>();
        private ICheckInModelFactory _modelFactory = Mock.Of<ICheckInModelFactory>();
        private ILogger<CheckInRepository> _logger = Mock.Of<ILogger<CheckInRepository>>();

        public Builder WithRedisServerProvider(IRedisServerProvider serverProvider) {
            _redisServerProvider = serverProvider;
            return this;
        }

        public Builder WithCheckInModelFactory(ICheckInModelFactory modelFactory) {
            _modelFactory = modelFactory;
            return this;
        }

        public Builder WithILogger(ILogger<CheckInRepository> logger) {
            _logger = logger;
            return this;
        }

        public CheckInRepository Build() {
            return new CheckInRepository(_redisServerProvider, _modelFactory, _logger);
        }

    }
}
