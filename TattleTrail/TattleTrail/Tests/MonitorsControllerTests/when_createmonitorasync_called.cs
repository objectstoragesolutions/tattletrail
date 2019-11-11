using AutoFixture;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TattleTrail.Controllers;
using TattleTrail.DAL;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.MonitorsControllerTests {
    [Subject(typeof(MonitorsController))]
    public class when_createmonitorasync_called {

        
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitorDetails = fixture.Create<MonitorDetails>();
            _factory = Mock.Of<IMonitorModelFactory>(x => x.Create(monitorDetails) == new MonitorProcess());
            _repository = Mock.Of<IRepository>();
            _controller = new Builder().WithModelFactory(_factory).WithRepository(_repository).Build();
        };
        Because of = async () => 
            result = await _controller.CreateMonitorAsync(monitorDetails);
        
        It should_create_new_monitor = () => 
            result.ShouldBeOfExactType(typeof(ObjectResult));

        static MonitorsController _controller;
        static MonitorDetails monitorDetails;
        static IRepository _repository;
        static IMonitorModelFactory _factory;
        static IActionResult result;
    }
}
