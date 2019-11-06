using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System.Collections.Generic;
using TattleTrail.DAL.RedisKeyValueProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisKeyValueProviderTests {
    [Subject(typeof(RedisKeyValueProvider))]
    [Ignore("Will rework soon")]
    public class when_getting_key_value_dictionary {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var patternSearch = fixture.Create<string>();
            IEnumerable<RedisKey> redisKey = new List<RedisKey>() { fixture.Create<RedisKey>() };
            server = Mock.Of<IServer>();
            database = Mock.Of<IDatabase>();
            //Mock.Get(server).Setup(x => x.Keys(pattern: "patternSearch")).Returns(redisKey);
            keyValueProvider = new Builder().Build();
        };
        Because of = () => 
            result = keyValueProvider.GetKeyValues(server, database);

        It should_return_valid_key_value_dictionary = () => { }; 

        static IRedisKeyValueProvider keyValueProvider;
        static IServer server;
        static IDatabase database;
        static Dictionary<RedisKey, RedisValue> result;
    }
}
