using AutoFixture;
using Machine.Specifications;
using Moq;
using System;
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
            subscribers = new string[] { };//fixture.Create<string[]>();
            factory = Mock.Of<MonitorModelFactory>();
        };
        Because of = () => 
            result = factory.Create(processName, intervalTime, subscribers);

        It should_create_monitor_process = () => {
            result.MonitorDetails.ProcessName.Equals(processName);
            result.MonitorDetails.LifeTime.Equals(intervalTime);
            result.MonitorDetails.Subscribers.Equals(subscribers);
        };

        static MonitorProcess result;
        static String processName;
        static int intervalTime;
        static string[] subscribers;
        static MonitorModelFactory factory;
    }
}
