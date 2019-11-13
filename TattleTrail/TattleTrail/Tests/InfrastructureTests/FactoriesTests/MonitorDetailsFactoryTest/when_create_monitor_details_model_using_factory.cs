using AutoFixture;
using Machine.Specifications;
using Moq;
using System;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.FactoriesTests.MonitorDetailsFactoryTest {
    [Subject(typeof(MonitorDetailsFactory))]
    public class when_create_monitor_details_model_using_factory {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            dataSource = Mock.Of<MonitorDetails>(x => x.IntervalTime == fixture.Create<Int32>() && 
                                                      x.ProcessName == fixture.Create<String>() && 
                                                      x.Subscribers == fixture.Create<String[]>());
            detailsFactory = Mock.Of<MonitorDetailsFactory>();
        };

        Because of = () => 
            result = detailsFactory.Create(dataSource);

        It should_create_monitor_detail = () =>
            result.ShouldNotBeNull();
        
        It should_create_monitor_detail_with_mentioned_lifetime = () => 
            result.IntervalTime.ShouldEqual(dataSource.IntervalTime);

        It should_create_monitor_detail_with_mentioned_processname = () =>
            result.ProcessName.ShouldEqual(dataSource.ProcessName);

        It should_create_monitor_detail_with_mentioned_subscribers = () =>
            result.Subscribers.ShouldEqual(dataSource.Subscribers);

        static IMonitorDetailsFactory detailsFactory;
        static MonitorDetails dataSource;
        static MonitorDetails result;
    }
}
