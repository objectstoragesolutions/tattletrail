using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.ExtensionsTests {
    [Subject(typeof(MonitorProcessModelExtensions))]
    public class when_converting_monitorprocess_model_to_hash_entry {

        Establish _context = () => {
            Fixture fixture = new Fixture();
            var lifeTime = fixture.Create<int>();
            var processName = fixture.Create<String>();
            monitor = Mock.Of<MonitorProcess>(
                x => x.Id == Guid.NewGuid() && 
                x.LifeTime == lifeTime && 
                x.ProcessName == processName && 
                x.Subscribers == new HashSet<string>());
        };

        Because of = () => 
            result = monitor.ConvertMonitorToHashEntry();

        It should_contain_process_field_name = () =>
            result[0].Name.Equals(nameof(monitor.ProcessName));

        It should_contain_process_field_value = () =>
            result[0].Value.Equals(monitor.ProcessName);

        It should_contain_lifeTime_field_name = () => 
            result[1].Name.Equals(nameof(monitor.LifeTime));

        It should_contain_lifeTime_field_value = () =>
            result[1].Value.Equals(monitor.LifeTime);

        It should_contain_subscribers_field_name =() =>
            result[2].Name.Equals(nameof(monitor.Subscribers));

        It should_contain_subscribers_field_value = () =>
             result[2].Value.Equals(monitor.Subscribers);

        static MonitorProcess monitor;
        static HashEntry[] result;
    }
}
