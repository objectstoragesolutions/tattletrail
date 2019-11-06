using Moq;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerInfoProvider;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    public class Builder {
        IConnectionMultiplexer _multiplexer = Mock.Of<IConnectionMultiplexer>();
        public Builder WithConnectionMultiplexer(IConnectionMultiplexer multiplexer) {
            _multiplexer = multiplexer;
            return this;
        }

        public RedisServerProvider Build() {
            return new RedisServerProvider(_multiplexer);
        }
    }
}
