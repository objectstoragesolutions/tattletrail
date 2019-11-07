using Moq;
using TattleTrail.DAL.RedisDataProvider;
using TattleTrail.DAL.RedisServerInfoProvider;

namespace TattleTrail.Tests.DALTests.RedisKeyValueProviderTests {
    public class Builder {
        public IRedisServerProvider _serverProvider = Mock.Of<IRedisServerProvider>();

        public Builder WithRedisServerProvider(IRedisServerProvider serverProvider) {
            _serverProvider = serverProvider;
            return this;
        }
        public IRedisDataProvider Build() { 
            return new RedisDataProvider(_serverProvider);
        }
    }
}
