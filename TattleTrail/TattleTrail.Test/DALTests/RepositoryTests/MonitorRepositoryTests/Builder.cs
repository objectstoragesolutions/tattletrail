using Microsoft.Extensions.Logging;
using Moq;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;

namespace TattleTrail.Test.DALTests.RepositoryTests.MonitorRepositoryTests {
    public class Builder {
        private IRedisServerProvider _redisServerProvider = Mock.Of<IRedisServerProvider>();
        private ILogger<MonitorRepository> _logger = Mock.Of<ILogger<MonitorRepository>>();

        public Builder WithRedisServerProvider(IRedisServerProvider serverProvider) {
            _redisServerProvider = serverProvider;
            return this;
        }

        public Builder WithILogger(ILogger<MonitorRepository> logger) {
            _logger = logger;
            return this;
        }

        public MonitorRepository Build() {
            return new MonitorRepository(_redisServerProvider, _logger);
        }
    }
}
