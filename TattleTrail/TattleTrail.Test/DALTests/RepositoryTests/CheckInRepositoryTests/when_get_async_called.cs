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

namespace TattleTrail.Test.DALTests.RepositoryTests.CheckInRepositoryTests {
    [Subject(typeof(CheckInRepository))]
    public class when_get_async_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            redisKey = key.ToString();
            hashEntry = new HashEntry[] { };
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None) == Task.FromResult(hashEntry));
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.GetAsync(redisKey);

        It should_get_all_hash_keys_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashGetAllAsync(redisKey, CommandFlags.None), Times.Once);

        static ICheckInRepository<CheckIn> repository;
        static IRedisServerProvider serverProvider;
        static HashEntry[] hashEntry;
        static RedisKey redisKey;
        static Guid key;
    }
}
