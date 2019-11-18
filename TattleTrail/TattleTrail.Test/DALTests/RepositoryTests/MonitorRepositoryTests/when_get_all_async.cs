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
    [Ignore("rework")]
    public class when_get_all_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            hashEntry = new HashEntry(nameof(MonitorProcess.Id), key.ToString());
            hashEntryArray = new HashEntry[] { hashEntry };
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None) == Task.FromResult(hashEntryArray));
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                result = await repository.GetAllAsync();

        It should_get_all_hash_keys_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IRedisServerProvider serverProvider;
        static HashEntry[] hashEntryArray;
        static RedisKey redisKey;
        static HashEntry hashEntry;
        static HashSet<MonitorProcess> result;
        static Guid key;
    }
}
