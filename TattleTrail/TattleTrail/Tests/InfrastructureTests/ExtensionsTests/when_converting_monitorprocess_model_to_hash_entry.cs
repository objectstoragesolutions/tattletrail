﻿using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.ExtensionsTests {
    [Subject(typeof(MonitorProcessModelExtensions))]
    public class when_converting_monitorprocess_model_to_hash_entry {

        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>(
                x => x.Id == Guid.NewGuid() && 
                x.MonitorDetails == 
                    new MonitorDetails() { LifeTime = fixture.Create<int>(), 
                         ProcessName = fixture.Create<String>(), 
                         Subscribers = fixture.Create<String[]>()
                    });
        };

        Because of = () => 
            result = monitor.ConvertMonitorToHashEntry();

        It should_contain_process_field_name = () =>
            result[0].Name.Equals(nameof(monitor.MonitorDetails.ProcessName));

        It should_contain_process_field_value = () =>
            result[0].Value.Equals(monitor.MonitorDetails.ProcessName);

        It should_contain_lifeTime_field_name = () => 
            result[1].Name.Equals(nameof(monitor.MonitorDetails.LifeTime));

        It should_contain_lifeTime_field_value = () =>
            result[1].Value.Equals(monitor.MonitorDetails.LifeTime);

        It should_contain_subscribers_field_name =() =>
            result[2].Name.Equals(nameof(monitor.MonitorDetails.Subscribers));

        It should_contain_subscribers_field_value = () =>
             result[2].Value.Equals(monitor.MonitorDetails.Subscribers);

        static MonitorProcess monitor;
        static HashEntry[] result;
    }
}
