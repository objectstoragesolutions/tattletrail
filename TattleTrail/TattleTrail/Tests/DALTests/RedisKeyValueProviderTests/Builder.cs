using TattleTrail.DAL.RedisKeyValueProvider;

namespace TattleTrail.Tests.DALTests.RedisKeyValueProviderTests {
    public class Builder {
        public IRedisKeyValueProvider Build() { 
            return new RedisKeyValueProvider();
        }
    }
}
