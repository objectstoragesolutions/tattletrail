﻿using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    [Ignore("rework")]
    public class when_delete_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>();
            serverProvider = Mock.Of<IRedisServerProvider>();
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.DeleteAsync(monitor.Id);

        It should_add_monitor_to_redis = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.KeyDeleteAsync(monitor.Id.ToString(), CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static MonitorProcess monitor;
        static IRedisServerProvider serverProvider;
    }
}
