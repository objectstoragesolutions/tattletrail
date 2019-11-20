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

namespace TattleTrail.Tests.DALTests.RepositoryTests.CheckInRepositoryTests {
    [Subject(typeof(CheckInRepository))]
    public class when_get_all_async_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            redisKeys = new RedisKey[] { redisKey };
            server = Mock.Of<IServer>(x => x.Keys(0, "checkinid:*", 10, 0, 0, CommandFlags.None) == redisKeys);
            database = Mock.Of<IDatabase>(x => x.HashGetAllAsync(Moq.It.IsAny<RedisKey>(), CommandFlags.None) == Task.FromResult(new HashEntry[] { }));
            serverProvider = Mock.Of<IRedisServerProvider> (x => x.Database == database && x.Server == server);
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
            await repository.GetAllAsync();

        It should_get_keys_by_pattern = () =>
                Mock.Get(serverProvider).Verify(x => x.Server.Keys(0, "checkinid:*", 10, 0, 0, CommandFlags.None), Times.Once);

        It should_call_get_async_n_times = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Exactly(redisKeys.Count()));

        static ICheckInRepository<CheckIn> repository;
        static IRedisServerProvider serverProvider;
        static IDatabase database;
        static IServer server;
        static IEnumerable<RedisKey> redisKeys;
        static RedisKey redisKey;
        static Guid key;

    }
}
