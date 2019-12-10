using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    public class when_get_all_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            redisKeys = new RedisKey[] { redisKey };
            hashEntry = new HashEntry(nameof(MonitorProcess.Id), key.ToString());
            hashEntryArray = new HashEntry[] { hashEntry };
            server = Mock.Of<IServer>(x => x.Keys(0, Moq.It.IsAny<RedisValue>(), 10, 0, 0, CommandFlags.None) == redisKeys);
            database = Mock.Of<IDatabase>(x => x.HashGetAllAsync(redisKey, CommandFlags.None) == Task.FromResult(hashEntryArray));
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database == database && x.Server == server);
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.GetAllAsync();

        It should_get_all_keys_by_pattern_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Server.Keys(0, "*", 10, 0, 0, CommandFlags.None), Times.Once);

        It should_get_all_hash_keys_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Exactly(redisKeys.Count()));

        static IMonitorRepository<MonitorProcess> repository;
        static IRedisServerProvider serverProvider;
        static IDatabase database;
        static IServer server;
        static HashEntry[] hashEntryArray;
        static RedisKey redisKey;
        static IEnumerable<RedisKey> redisKeys;
        static HashEntry hashEntry;
        static Guid key;
    }
}
