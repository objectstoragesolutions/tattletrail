using AutoFixture;
using Machine.Specifications;
using Moq;
using System;
using System.Collections.Generic;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.FactoriesTests {
    [Subject(typeof(MonitorModelFactory))]
    public class when_create_monitor_model_using_factory {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            processName = fixture.Create<String>();
            intervalTime = fixture.Create<int>();
            subscribers = fixture.Create<HashSet<string>>();
            factory = Mock.Of<MonitorModelFactory>();
        };
        Because of = () => 
            result = factory.Create(processName, intervalTime, subscribers);

        It should_create_monitor_process = () => {
            result.ProcessName.Equals(processName);
            result.LifeTime.Equals(intervalTime);
            result.Subscribers.Equals(subscribers);
        };

        static MonitorProcess result;
        static String processName;
        static int intervalTime;
        static HashSet<string> subscribers;
        static MonitorModelFactory factory;
    }
}
