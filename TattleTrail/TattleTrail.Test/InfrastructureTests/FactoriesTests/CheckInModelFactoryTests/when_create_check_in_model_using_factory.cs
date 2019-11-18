using AutoFixture;
using Machine.Specifications;
using Moq;
using System;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.InfrastructureTests.FactoriesTests.CheckInModelFactoryTests {
    [Subject(typeof(CheckInModelFactory))]
    public class when_create_check_in_model_using_factory {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<Guid>();
            checkInFactory = Mock.Of<CheckInModelFactory>();
        };

        Because of = () => 
            result = checkInFactory.Create(id);

        It should_return_new_check_in_with_passed_monitor_id = () => {
            result.CheckInId.ShouldNotBeTheSameAs(Moq.It.IsAny<Guid>());
            result.MonitorId.ShouldEqual(id);
        };

        static ICheckInModelFactory checkInFactory;
        static Guid id;
        static CheckIn result;
    }
}
