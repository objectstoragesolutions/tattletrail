using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.CheckInRepositoryTests {
    [Subject(typeof(CheckInRepository))]
    public class when_create_async_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            key = fixture.Create<Guid>();
            interval = fixture.Create<Int32>();
            checkIn = Mock.Of<CheckIn>();
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database.HashSetAsync(checkIn.CreateKeyString(), 
                new HashEntry[] { new HashEntry(nameof(CheckIn.MonitorId), checkIn.MonitorId.ToString()) }, CommandFlags.None) == Task.FromResult(true) && 
                x.Database.KeyExpire(checkIn.CreateKeyString(), TimeSpan.FromSeconds(interval), CommandFlags.None) == true);
            modelFactory = Mock.Of<ICheckInModelFactory>(x => x.Create(key) == checkIn);
            repository = new Builder().WithRedisServerProvider(serverProvider)/*.WithCheckInModelFactory(modelFactory)*/.Build();
        };

        Because of = async () => 
            await repository.CreateAsync(key,interval);

        It should_create_chech_in_model_using_factory = () =>
                Mock.Get(modelFactory).Verify(x => x.Create(key), Times.Once);

        It should_add_hash_set_async_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashSetAsync(checkIn.CreateKeyString(),
                        new HashEntry[] { new HashEntry(nameof(CheckIn.MonitorId), checkIn.MonitorId.ToString()) }, CommandFlags.None), Times.Once);

        It should_set_expire_async_once = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.KeyExpireAsync(checkIn.CreateKeyString(), TimeSpan.FromSeconds(interval), CommandFlags.None), Times.Once);

        static ICheckInRepository<CheckIn> repository;
        static IRedisServerProvider serverProvider;
        static ICheckInModelFactory modelFactory;
        static CheckIn checkIn;
        static Int32 interval;
        static Guid key;
    }
}
