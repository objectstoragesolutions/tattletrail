using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System.Collections.Generic;
using TattleTrail.DAL.RedisDataProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisKeyValueProviderTests {
    [Subject(typeof(RedisDataProvider))]
    [Ignore("Will rework soon")]
    public class when_getting_key_value_dictionary {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var patternSearch = fixture.Create<string>();
            IEnumerable<RedisKey> redisKey = new List<RedisKey>() { fixture.Create<RedisKey>() };
            //Mock.Get(server).Setup(x => x.Keys(pattern: "patternSearch")).Returns(redisKey);
            keyValueProvider = new Builder().Build();
        };
        Because of = () => 
            result = keyValueProvider.GetKeyValues();

        It should_return_valid_key_value_dictionary = () => { }; 

        static IRedisDataProvider keyValueProvider;
        static Dictionary<RedisKey, RedisValue> result;
    }
}
