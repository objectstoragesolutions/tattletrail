using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    public class when_create_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>();
            hashEntry = monitor.ConvertMonitorToHashEntry();
            redisKey = monitor.Id.ToString();
            database = Mock.Of<IDatabase>(x => x.HashSetAsync(redisKey, hashEntry, CommandFlags.None) == Task.CompletedTask);
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.CreateAsync(monitor);

        It should_add_monitor_to_redis = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashSetAsync(redisKey, hashEntry, CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IDatabase database;
        static HashEntry[] hashEntry;
        static RedisKey redisKey;
        static MonitorProcess monitor;
        static IRedisServerProvider serverProvider;
    }
}
