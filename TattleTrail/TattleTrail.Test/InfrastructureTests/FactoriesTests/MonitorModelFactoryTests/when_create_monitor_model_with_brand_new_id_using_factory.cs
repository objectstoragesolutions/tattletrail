using AutoFixture;
using Machine.Specifications;
using Moq;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.InfrastructureTests.FactoriesTests.MonitorModelFactoryTests {
    [Subject(typeof(MonitorModelFactory))]
    public class when_create_monitor_model_with_brand_new_id_using_factory {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitorDetails = fixture.Create<MonitorDetails>();
            detailsFactory = Mock.Of<IMonitorDetailsFactory>(x => x.Create(monitorDetails) == monitorDetails);
            factory = new Builder().WithMonitorDetailsFactory(detailsFactory).Build();
        };
        Because of = () => 
            result = factory.Create(monitorDetails);

        It should_call_monitor_details_factory_once = () =>
            Mock.Get(detailsFactory).Verify(x => x.Create(monitorDetails), Times.Once);

        It should_create_monitor_process_with_passed_monitor_details= () =>
            result.MonitorDetails.ShouldEqual(monitorDetails);


        static MonitorProcess result;
        static MonitorDetails monitorDetails;
        static IMonitorDetailsFactory detailsFactory;
        static IMonitorModelFactory factory;
    }
}
