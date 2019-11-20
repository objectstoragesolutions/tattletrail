using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    public class when_get_async_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            hashEntry = fixture.Create<HashEntry>();
            redisHash = new HashEntry []{ hashEntry };
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None) == Task.FromResult(redisHash));
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.GetAsync(redisKey);

        It should_get_all_hash_keys_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IRedisServerProvider serverProvider;
        static HashEntry[] redisHash;
        static RedisKey redisKey;
        static HashEntry hashEntry;
        static Guid key;
    }
}
