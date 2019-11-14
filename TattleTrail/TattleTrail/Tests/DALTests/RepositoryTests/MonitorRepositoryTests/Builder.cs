using Moq;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    public class Builder {
        private IRedisServerProvider _redisServerProvider = Mock.Of<IRedisServerProvider>();

        public Builder WithRedisServerProvider(IRedisServerProvider serverProvider) {
            _redisServerProvider = serverProvider;
            return this;
        }

        public MonitorRepository Build() {
            return new MonitorRepository(_redisServerProvider);
        }

    }
}
