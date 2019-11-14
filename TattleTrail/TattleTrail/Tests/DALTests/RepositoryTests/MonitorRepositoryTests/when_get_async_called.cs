using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    [Ignore("Rework")]
    public class when_get_async_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            redisKeys = new RedisKey []{ redisKey };
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Server.Keys(Moq.It.IsAny<int>(), "*", Moq.It.IsAny<int>(), Moq.It.IsAny<CommandFlags>()) == Task.FromResult(redisKeys));
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.GetAsync(redisKey);

        It should_get_all_hash_keys_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IRedisServerProvider serverProvider;
        static IEnumerable<RedisKey> redisKeys;
        static RedisKey redisKey;
        static Guid key;
    }
}
